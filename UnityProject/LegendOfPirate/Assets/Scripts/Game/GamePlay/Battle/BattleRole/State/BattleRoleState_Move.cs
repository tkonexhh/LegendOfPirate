using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace GameWish.Game
{
    public class BattleRoleState_Move : BattleRoleState
    {
        private BattleRoleController m_Target;

        public override void Enter(BattleRoleController controller)
        {
            base.Enter(controller);
            controller.renderer.CrossFadeAnim("Run", 0.1f, true);
        }

        public override void Execute(BattleRoleController controller, float dt)
        {
            if (m_Target == null)
                return;

            controller.AIDestination.target = m_Target.transform;
        }

        public override void Exit(BattleRoleController controller)
        {

        }

        public override void OnMsg(BattleRoleController controller, int key, params object[] args)
        {
            m_Target = (BattleRoleController)args[0];
        }
    }

}