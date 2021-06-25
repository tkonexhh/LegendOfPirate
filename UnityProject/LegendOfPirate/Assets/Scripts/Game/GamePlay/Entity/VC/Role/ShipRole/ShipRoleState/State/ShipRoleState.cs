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
        Wander,
        Training,
        Reading,
        Fishing,
    }

    public class ShipRoleState : FSMState<ShipRoleController>
    {
        protected ShipRoleController m_ShipRoleController = null;

        public ShipRoleStateId stateID
        {
            get;
            set;
        }

        public ShipRoleState(ShipRoleStateId stateEnum)
        {
            stateID = stateEnum;
        }

        public override void Enter(ShipRoleController entity)
        {
            base.Enter(entity);

            m_ShipRoleController = entity;
        }
    }

}