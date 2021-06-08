using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipUnitStateMachine : FSMStateMachine<ShipUnit>
    {
        public ShipUnitStateMachine(ShipUnit entity) : base(entity)
        {
            stateFactory = new FSMStateFactory<ShipUnit>(false);

            stateFactory.RegisterState(ShipUnitStateId.Locked, new ShipUnitStateLocked(ShipUnitStateId.Locked));
            stateFactory.RegisterState(ShipUnitStateId.ReadyToBuild, new ShipUnitStateReadyToBuild(ShipUnitStateId.ReadyToBuild));
            stateFactory.RegisterState(ShipUnitStateId.Building, new ShipUnitStateBuilding(ShipUnitStateId.Building));
            stateFactory.RegisterState(ShipUnitStateId.Idle, new ShipUnitStateIdle(ShipUnitStateId.Idle));
        }
    }

}