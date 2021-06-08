using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIState_Victory : BattleRoleAIState
    {
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            ai.controller.Renderer.PlayAnim(BattleDefine.ROLEANIM_VICTORY);
            ai.controller.MonoReference.AstarAI.canMove = false;
        }
    }

}