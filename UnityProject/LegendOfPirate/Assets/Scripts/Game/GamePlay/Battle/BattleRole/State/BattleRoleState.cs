using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleState : FSMState<BattleRoleController>
    {
        protected BattleRoleController m_Controller;

        public override void Enter(BattleRoleController controller)
        {
            m_Controller = controller;
        }

        public override void Execute(BattleRoleController controller, float dt)
        {

        }

        public override void Exit(BattleRoleController controller)
        {

        }

        public override void OnMsg(BattleRoleController controller, int key, params object[] args)
        {

        }
    }

}