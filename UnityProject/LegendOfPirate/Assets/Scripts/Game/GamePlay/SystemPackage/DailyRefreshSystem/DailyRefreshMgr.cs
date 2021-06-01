using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public class DailyRefreshMgr : TSingleton<DailyRefreshMgr>, IMgr
    {
        private int m_CurItemId = 0;
        private Dictionary<int, DailyRefreshItem> m_DailyRefreshItemDic = null;
        private int m_LastCheckHour = -1;
        private int m_TimerId;

        #region IMgr

        public void OnInit()
        {
            m_DailyRefreshItemDic = new Dictionary<int, DailyRefreshItem>();

            m_TimerId = StartTimeChecker();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_DailyRefreshItemDic.Clear();
            m_DailyRefreshItemDic = null;

            Timer.S.Cancel(m_TimerId);
        }

        #endregion

        #region Public

        public int Register(DateTime lastRefreshTime, int refreshHour, Action callback)
        {
            m_CurItemId++;

            DailyRefreshItem item = DailyRefreshItem.Allocate();
            item.SetParams(m_CurItemId, lastRefreshTime, refreshHour, callback);
            m_DailyRefreshItemDic.Add(m_CurItemId, item);

            return m_CurItemId;
        }

        public void Unregister(int id)
        {
            if (m_DailyRefreshItemDic.ContainsKey(id))
            {
                m_DailyRefreshItemDic.Remove(id);
            }
            else
            {
                Log.e("Item not found: " + id);
            }
        }

        #endregion

        #region Private

        private int StartTimeChecker()
        {
            int id = Timer.S.Post2Really(OnMinuteChanged, 60, -1);
            return id;
        }

        private void OnMinuteChanged(int tick)
        {
            if (DateTime.Now.Hour != m_LastCheckHour)
            {
                foreach(DailyRefreshItem item in m_DailyRefreshItemDic.Values)
                {
                    bool needRefresh = CheckNeedRefresh(item);
                }

                m_LastCheckHour = DateTime.Now.Hour;
            }
        }

        private bool CheckNeedRefresh(DailyRefreshItem item)
        {
            if (item.LastRefreshDate.Day < DateTime.Now.Day) // Last Refresh Not In Same Day
            {
                if (DateTime.Now.Hour >= item.RefreshHour)
                {
                    return true;
                }
            }
            else if (item.LastRefreshDate.Day == DateTime.Now.Day) // Last Refresh In Same Day
            {
                if (item.LastRefreshDate.Hour < DateTime.Now.Hour && DateTime.Now.Hour >= item.RefreshHour)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }

}