using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAIState_Skill : BattleRoleAIState
    {
        private Skill m_CstSkill;
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);

            m_CstSkill = ai.controller.Skill.GetReadySkill();
            if (m_CstSkill == null)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
                return;
            }

            ai.controller.renderer.CrossFadeAnim("Attack01", 0.1f, true);
            m_CstSkill.Cast(ai.controller);
            //TODO 动画播放结束后退出状态
            Timer.S.Post2Scale(i =>
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
            }, 2);

        }

    }

}