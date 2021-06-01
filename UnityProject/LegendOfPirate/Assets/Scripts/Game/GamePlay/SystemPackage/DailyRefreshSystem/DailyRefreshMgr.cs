using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public class DailyRefreshMgr : TSingleton<DailyRefreshMgr>, IMgr
    {
        private List<DailyRefreshItem> m_DailyRefreshItemList = null;

        #region IMgr

        public void OnInit()
        {
            m_DailyRefreshItemList = new List<DailyRefreshItem>();

            StartTimeChecker();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_DailyRefreshItemList = null;
        }

        #endregion

        #region Public

        public void Register(DateTime lastRefreshTime, int refreshHour, Action callback)
        {

        }

        #endregion

        #region Private

        private void StartTimeChecker()
        {
            Timer.S.Post2Really(OnMinuteChanged, 60, -1);
        }

        private void OnMinuteChanged(int tick)
        {
            for (int i = 0; i < m_DailyRefreshItemList.Count; i++)
            {

            }
        }

        #endregion
    }

}