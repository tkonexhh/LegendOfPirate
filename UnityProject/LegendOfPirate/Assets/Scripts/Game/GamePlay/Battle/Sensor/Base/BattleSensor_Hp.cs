using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式 - 血量
    /// </summary>
    public class BattleSensor_Hp : BattleSensor
    {

        public BattleSensor_Hp(BattleRoleController controller) : base(controller)
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