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
        private PickTargetType m_PickTargetType;

        public BattleSensor(PickTargetType type)
        {
            m_PickTargetType = type;
        }

        protected void GetPickBattleCamp(BattleRoleController picker)
        {
            if (m_PickTargetType == PickTargetType.Enemy)
            {
                m_OppositeCamp = BattleHelper.GetOppositeCamp(picker.camp);
            }
            else
            {
                m_OppositeCamp = picker.camp;
            }
        }

        public abstract BattleRoleController PickTarget(BattleRoleController picker);
        public virtual BattleRoleController[] PickTarget(BattleRoleController picker, int num)
        {
            return null;
        }
    }

}