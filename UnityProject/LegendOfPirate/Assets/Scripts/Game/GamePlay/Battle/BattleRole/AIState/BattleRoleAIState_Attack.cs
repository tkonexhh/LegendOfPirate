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
            ai.controller.Renderer.CrossFadeAnim(BattleDefine.ROLEANIM_IDLE, 0.1f);
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
                PlayAttackAnim();
                m_AttackTimer = 0;
            }

            //检测Target是否太远
            if (Vector3.Distance(ai.controller.transform.position, ai.Target.transform.position) > ai.controller.Data.buffedData.AttackRange + 0.05f)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
            }
        }

        public override void Exit(BattleRoleAI ai)
        {
            base.Exit(ai);
            m_AttackTimer = 0;
        }

        private void PlayAttackAnim()
        {
            m_AI.controller.Renderer.CrossFadeAnim(BattleDefine.ROLEANIM_ATTACK01, 0.1f);
            //TODO 需要改成动画事件
            m_AI.controller.Data.Attacker.Attack(m_AI.controller);
        }


    }

}