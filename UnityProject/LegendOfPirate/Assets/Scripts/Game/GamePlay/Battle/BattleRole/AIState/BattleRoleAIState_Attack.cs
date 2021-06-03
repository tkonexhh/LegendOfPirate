using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIState_Attack : BattleRoleAIState
    {
        private float m_AttackTimer;
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            m_AttackTimer = 0;
            ai.controller.renderer.CrossFadeAnim("Idle", 0.1f, true);
        }

        public override void Execute(BattleRoleAI ai, float dt)
        {
            base.Execute(ai, dt);

            if (ai.Target == null)
            {
                return;//ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            }

            m_AttackTimer += dt;
            //TODO 攻击速度从Data中读取
            if (m_AttackTimer >= 2.1f)
            {
                Attack();
                m_AttackTimer = 0;
            }



            //检测Target是否太远
            if (Vector3.Distance(ai.controller.transform.position, ai.Target.transform.position) > 5.0f)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
            }


        }

        public override void Exit(BattleRoleAI ai)
        {
            base.Exit(ai);
            m_AttackTimer = 0;
        }

        private void Attack()
        {
            m_AI.controller.renderer.CrossFadeAnim("Attack02", 0.1f, false);
            CreateDamage();
        }

        //TODO 需要改成动画事件
        private void CreateDamage()
        {
            if (m_AI.onAttack != null)
            {
                m_AI.onAttack();
            }

            int damage = BattleHelper.CalcAtkDamage(m_AI.controller.Data.buffedData);
            RoleDamagePackage damagePackage = new RoleDamagePackage();
            damagePackage.damageType = BattleDamageType.Normal;
            damagePackage.damage = damage;
            BattleMgr.S.SendDamage(m_AI.Target, damagePackage);
        }
    }

}