using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式
    /// </summary>
    public abstract class BattleSensor
    {
        public BattleSensor()
        {

        }


        public abstract EntityBase PickTarget();
    }

}