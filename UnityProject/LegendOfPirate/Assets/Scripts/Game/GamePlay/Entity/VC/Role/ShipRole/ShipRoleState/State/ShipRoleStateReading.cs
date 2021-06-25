﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipRoleStateReading : ShipRoleState
    {
        public ShipRoleStateReading(ShipRoleStateId stateEnum) : base(stateEnum)
        {

        }

        public override void Enter(ShipRoleController entity)
        {
            base.Enter(entity);

            m_ShipRoleController.RoleView.PlayAnim(BattleDefine.ROLEANIM_IDLE);
        }
    }

}