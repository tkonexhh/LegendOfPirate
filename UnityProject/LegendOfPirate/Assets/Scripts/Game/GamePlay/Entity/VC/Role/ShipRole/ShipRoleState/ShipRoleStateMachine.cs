using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipRoleStateMachine : FSMStateMachine<ShipRoleController>
    {
        public ShipRoleStateMachine(ShipRoleController entity) : base(entity)
        {
            stateFactory = new FSMStateFactory<ShipRoleController>(false);

            stateFactory.RegisterState(ShipRoleStateId.Idle, new ShipRoleStateIdle(ShipRoleStateId.Idle));
        }
    }

}