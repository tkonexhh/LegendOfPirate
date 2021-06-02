using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 战场索敌方式
    /// </summary>
    public abstract class BattleSensor : IBattleSensor
    {
        // protected BattleRoleController m_Controller;
        protected BattleCamp m_OppositeCamp;



        public abstract BattleRoleController PickTarget(BattleRoleController picker);
        public virtual BattleRoleController[] PickTarget(BattleRoleController picker, int num)
        {
            return null;
        }
    }

}