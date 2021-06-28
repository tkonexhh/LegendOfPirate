using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAIState_Skill : BattleRoleAIState
    {
        private Skill m_CastSkill;

        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            ai.controller.MonoReference.AstarAI.canMove = false;
            m_CastSkill = ai.controller.Skill.GetReadySkill();
            if (m_CastSkill == null)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Attack);
                return;
            }
            //动画进栈
            ai.controller.Renderer.PushAnimFade(BattleDefine.ROLEANIM_SKILL01, 0.1f);
            m_CastSkill.onSkillEnd += OnSkillEnd;
            m_CastSkill.Cast();
        }


        public override void Exit(BattleRoleAI ai)
        {
            ai.controller.MonoReference.AstarAI.canMove = true;
            if (m_CastSkill != null)
            {
                m_CastSkill.onSkillEnd -= OnSkillEnd;
            }
        }

        private void OnSkillEnd()
        {
            m_AI.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Attack);
        }

    }

}