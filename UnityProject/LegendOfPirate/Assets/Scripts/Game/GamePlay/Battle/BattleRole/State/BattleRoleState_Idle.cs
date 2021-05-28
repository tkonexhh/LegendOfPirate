using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleState_Idle : BattleRoleState
    {
        public override void Enter(BattleRoleController controller)
        {
            base.Enter(controller);
            controller.renderer.PlayAnim("Idle", true);
        }

        public override void Execute(BattleRoleController controller, float dt)
        {

        }

        public override void Exit(BattleRoleController controller)
        {

        }
    }

}