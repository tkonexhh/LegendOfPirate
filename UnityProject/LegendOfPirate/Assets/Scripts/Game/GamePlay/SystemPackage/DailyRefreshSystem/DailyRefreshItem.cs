using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
	public class DailyRefreshItem : ICacheAble, ICacheType
	{
        private DateTime m_LastRefreshDate;
        private int m_RefreshHour = 0;

        #region ICacheAble,ICacheType

        public bool cacheFlag { get; set; }

        public void OnCacheReset()
        {
            m_LastRefreshDate = DateTime.MinValue;
        }

        public void Recycle2Cache()
        {
            ObjectPool<DailyRefreshItem>.S.Recycle(this);
        }

        #endregion

        public static DailyRefreshItem Allocate()
        {
            return ObjectPool<DailyRefreshItem>.S.Allocate();
        }
    }

}