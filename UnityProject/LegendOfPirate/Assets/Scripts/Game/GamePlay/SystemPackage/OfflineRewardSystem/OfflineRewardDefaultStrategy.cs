using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class OfflineRewardDefaultStrategy : IOfflineRewardStrategy
    {
        private double m_MaxOfflineTime = 2 * 60 * 60;//最大离线时间2小时
        public double GetReward(double offlineTime)
        {
            if (offlineTime > m_MaxOfflineTime)
            {
                offlineTime = m_MaxOfflineTime;
            }

            double incomePerSec = GetOfflineIncomePerSec();

            return incomePerSec * offlineTime;
        }

        private double GetOfflineIncomePerSec()
        {
            return 0;
        }
    }

}