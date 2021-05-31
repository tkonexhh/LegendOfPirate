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
        protected BattleRoleController m_Controller;
        protected BattleCamp m_OppositeCamp;

        public BattleSensor(BattleRoleController controller)
        {
            m_Controller = controller;

        }


        public abstract BattleRoleController PickTarget();
        public abstract BattleRoleController[] PickTarget(int num);
    }

}