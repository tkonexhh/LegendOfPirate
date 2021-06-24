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
            ai.controller.MonoReference.AstarAI.canMove = false;
            ai.controller.Renderer.modelMonoReference.onAnimAttack += OnAnimDealDamage;
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
            if (m_AttackTimer >= 2.1f &&
                !ai.controller.Data.buffedData.StatusMask.HasStatus(StatusControlType.AttackForbid))
            {
                PlayAttackAnim();
                m_AttackTimer = 0;
            }

            FaceToTarget();

            //检测Target是否太远
            if (Vector3.Distance(ai.controller.transform.position, ai.Target.transform.position) > ai.controller.Data.AtkRange + 0.05f)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
            }
        }

        public override void Exit(BattleRoleAI ai)
        {
            base.Exit(ai);
            m_AttackTimer = 0;
            ai.controller.MonoReference.AstarAI.canMove = true;
            ai.controller.Renderer.modelMonoReference.onAnimAttack -= OnAnimDealDamage;
        }

        private void PlayAttackAnim()
        {
            m_AI.controller.Renderer.CrossFadeAnim(BattleDefine.ROLEANIM_ATTACK01, 0.1f);
            // m_AI.controller.Data.Attacker.Attack(m_AI.controller, m_AI.Target);
        }

        private void FaceToTarget()
        {
            // Debug.LogError("FaceToTarget");
            m_AI.controller.transform.localRotation = Quaternion.RotateTowards(
                m_AI.controller.transform.localRotation,
                Quaternion.Euler(0, 180 - m_AI.Target.transform.rotation.y, 0),
                10.0f * Time.deltaTime);
            //(Vector3.up, 180 - m_AI.Target.transform.rotation.y);//旋转角色
        }

        private void OnAnimDealDamage()
        {
            m_AI.controller.Data.Attacker.Attack(m_AI.controller, m_AI.Target);
        }
    }

}