using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
	public class DailyRefreshItem : ICacheAble, ICacheType
	{
        private int m_Id = -1;
        private DateTime m_LastRefreshDate;
        private int m_RefreshHour = -1;
        private Action m_RefreshCallback;

        #region ICacheAble,ICacheType

        public bool cacheFlag { get; set; }
        public int Id { get => m_Id;}
        public DateTime LastRefreshDate { get => m_LastRefreshDate; }
        public int RefreshHour { get => m_RefreshHour; }

        public void OnCacheReset()
        {
            m_LastRefreshDate = DateTime.MinValue;
            m_RefreshCallback = null;
            m_RefreshHour = -1;
        }

        public void Recycle2Cache()
        {
            ObjectPool<DailyRefreshItem>.S.Recycle(this);
        }

        #endregion

        public static DailyRefreshItem Allocate()
        {
            DailyRefreshItem item = ObjectPool<DailyRefreshItem>.S.Allocate();

            Debug.Assert(item.m_Id == -1, "DailyRefreshItem m_Id not -1!");
            Debug.Assert(item.m_LastRefreshDate == DateTime.MinValue, "DailyRefreshItem m_LastRefreshDate Wrong!");
            Debug.Assert(item.m_RefreshCallback == null, "DailyRefreshItem m_RefreshCallback not null!");
            Debug.Assert(item.m_RefreshHour == -1, "DailyRefreshItem m_RefreshHour not -1!");

            return item;
        }

        public void SetParams(int id, DateTime lastRefreshTime, int refreshHour, Action refreshCallback)
        {
            m_Id = id;
            m_LastRefreshDate = lastRefreshTime;
            m_RefreshHour = refreshHour;
            m_RefreshCallback = refreshCallback;
        }

        public void Notify()
        {
            m_RefreshCallback?.Invoke();
        }
    }

}