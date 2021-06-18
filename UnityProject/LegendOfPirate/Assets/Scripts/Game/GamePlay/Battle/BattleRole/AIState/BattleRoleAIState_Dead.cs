using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAIState_Dead : BattleRoleAIState
    {
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            Debug.LogError("Death");
            ai.controller.Renderer.PlayAnim(BattleDefine.ROLEANIM_DEATH);
            BattleMgr.S.Role.RemoveController(ai.controller);
            ai.controller.MonoReference.AstarAI.canMove = false;
            ai.controller.MonoReference.Collider.enabled = false;
            //TODO 改为动画时间
            Timer.S.Post2Really(i =>
            {
                BattleRoleControllerFactory.RecycleBattleRole(ai.controller);
            }, 3);

        }


    }

}