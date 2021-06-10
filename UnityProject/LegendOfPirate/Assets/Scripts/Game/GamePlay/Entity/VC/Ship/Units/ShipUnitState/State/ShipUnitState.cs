using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum ShipUnitStateId
    {
        Locked,
        ReadyToBuild,
        Building,
        Idle,
    }

    public class ShipUnitState : FSMState<ShipUnit>
    {
        public ShipUnitStateId stateID
        {
            get;
            set;
        }

        public ShipUnitState(ShipUnitStateId stateEnum)
        {
            stateID = stateEnum;
        }
    }

}