using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_Self : BattleSensor
    {
        public BattleSensor_Self() : base(PickTargetType.Self) { }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            return picker;
        }
    }

}