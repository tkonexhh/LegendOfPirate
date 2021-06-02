using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式 - 百分比血量；
    /// </summary>
    public class BattleSensor_HpRate : BattleSensor
    {


        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            return null;
        }

        public override BattleRoleController[] PickTarget(BattleRoleController picker, int num)
        {
            return null;
        }
    }

}