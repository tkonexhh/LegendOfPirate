using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleState_Attack : BattleRoleState
    {
        public override void Enter(BattleRoleController controller)
        {
            base.Enter(controller);
            controller.renderer.PlayAnim("Attack01");
        }

        public override void Execute(BattleRoleController controller, float dt)
        {

        }

        public override void Exit(BattleRoleController controller)
        {

        }
    }

}