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

            ai.controller.Renderer.CrossFadeAnim(BattleDefine.ROLEANIM_SKILL01, 0.1f);
            m_CastSkill.Cast();
            //TODO 动画播放结束后退出状态
            Timer.S.Post2Scale(i =>
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Attack);
            }, 1.5f);

        }

        public override void Exit(BattleRoleAI ai)
        {
            ai.controller.MonoReference.AstarAI.canMove = true;
        }



    }

}