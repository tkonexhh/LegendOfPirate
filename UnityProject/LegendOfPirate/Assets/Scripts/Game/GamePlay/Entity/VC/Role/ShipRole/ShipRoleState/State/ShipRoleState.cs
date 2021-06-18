using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum ShipRoleStateId
    {
        None,
        Idle,
    }

    public class ShipRoleState : FSMState<ShipRoleController>
    {
        public ShipRoleStateId stateID
        {
            get;
            set;
        }

        public ShipRoleState(ShipRoleStateId stateEnum)
        {
            stateID = stateEnum;
        }
    }

}