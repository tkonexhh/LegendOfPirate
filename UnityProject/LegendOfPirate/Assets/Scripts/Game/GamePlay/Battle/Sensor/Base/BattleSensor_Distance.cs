using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式 - 距离；
    /// </summary>
    public class BattleSensor_Distance : BattleSensor
    {
        public BattleSensor_Distance(BattleRoleController controller) : base(controller)
        {
        }

        public override BattleRoleController PickTarget()
        {
            return null;
        }

        public override BattleRoleController[] PickTarget(int num)
        {
            return null;
        }
    }

}