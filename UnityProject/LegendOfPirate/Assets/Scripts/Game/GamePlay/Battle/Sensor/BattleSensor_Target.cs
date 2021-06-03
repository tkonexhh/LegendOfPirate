using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_Target : BattleSensor
    {
        public BattleSensor_Target() : base(PickTargetType.Target) { }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            return picker.AI.Target ?? new BattleSensor_Nearest(PickTargetType.Enemy).PickTarget(picker);
        }
    }

}