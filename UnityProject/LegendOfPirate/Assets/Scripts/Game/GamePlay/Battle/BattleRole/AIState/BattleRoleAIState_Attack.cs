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
                //动画进栈
                m_AI.controller.Renderer.PushAnimFade(BattleDefine.ROLEANIM_ATTACK01, 0.1f);
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

        private void FaceToTarget()
        {
            Vector3 faceDir = -m_AI.controller.transform.position + m_AI.Target.transform.position;
            faceDir.Normalize();
            // Debug.LogError("FaceDir:" + faceDir);
            // faceDir.x = faceDir.z = 0;
            Quaternion rotate = Quaternion.LookRotation(faceDir);

            // Debug.LogError(rotate);
            m_AI.controller.transform.localRotation = Quaternion.RotateTowards(
                m_AI.controller.transform.localRotation,
                rotate,
                30.0f * Time.deltaTime);
            //(Vector3.up, 180 - m_AI.Target.transform.rotation.y);//旋转角色
        }

        private void OnAnimDealDamage()
        {
            m_AI.controller.Data.Attacker.Attack(m_AI.controller, m_AI.Target);
        }
    }

}