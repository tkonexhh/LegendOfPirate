using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIState_Dead : BattleRoleAIState
    {
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            Debug.LogError("Death");
            ai.controller.Renderer.PlayAnim(BattleDefine.ROLEANIM_DEATH);
            BattleMgr.S.BattleRendererComponent.RemoveController(ai.controller);
            ai.controller.MonoReference.AstarAI.canMove = false;
        }


    }

}