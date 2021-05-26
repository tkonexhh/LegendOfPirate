using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式 - 嗜血索敌 - 寻找百分比血量最低的单位进行攻击；
    /// </summary>
    public class BattleSensor_Nearest : BattleSensor
    {

        public override IElement PickTarget()
        {
            return null;
        }
    }

}