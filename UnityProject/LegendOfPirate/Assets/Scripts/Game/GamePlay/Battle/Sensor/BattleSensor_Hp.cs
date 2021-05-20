using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式 - 欺凌索敌 - 寻找绝对血量最低的单位进行攻击
    /// </summary>
    public class BattleSensor_Hp : BattleSensor
    {

        public override EntityBase PickTarget()
        {
            return null;
        }
    }

}