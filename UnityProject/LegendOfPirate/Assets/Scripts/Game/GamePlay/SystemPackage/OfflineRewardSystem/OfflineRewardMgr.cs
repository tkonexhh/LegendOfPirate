using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public class OfflineRewardMgr : TSingleton<OfflineRewardMgr>, ITimeObserver
    {
        private IOfflineRewardStrategy m_OfflineRewardStargegy = new OfflineRewardDefaultStrategy();

        public IOfflineRewardStrategy OfflineReawrdStargegy {
            get {
                return m_OfflineRewardStargegy;
            }
        }

        public void Init()
        {
            TimeUpdateMgr.S.AddObserver(this);
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            //EventSystem.S.Register(EventID.OnEnterSpecialEvent, HandleEvent);
            //EventSystem.S.Register(EventID.OnExitSpecialEvent, HandleEvent);
        }

        private void UnregisterEvents()
        {
            //EventSystem.S.UnRegister(EventID.OnEnterSpecialEvent, HandleEvent);
            //EventSystem.S.UnRegister(EventID.OnExitSpecialEvent, HandleEvent);
        }

        private void HandleEvent(int id, params object[] param)
        {

        }

        #region TimeUpdateObserver
        public int GetTickInterval()
        {
            return 1;
        }

        public int GetTickCount()
        {
            return -1;
        }

        public int GetTotalSeconds()
        {
            return -1;
        }

        public void OnFinished()
        {
        }

        public void OnPause()
        {
        }

        public void OnResume()
        {
            CheckLoginOfflineReward();
        }

        public void OnStart()
        {
            CheckLoginOfflineReward();
        }

        public void OnTick(int count)
        {

        }

        public bool ShouldRemoveWhenMapChanged()
        {
            return false;
        }
        #endregion

        private void CheckLoginOfflineReward()
        {
            double offlineTime = GetOfflineSeconds(GameDataMgr.S.GetPlayerInfoData().lastPlayTime);
            float maxOfflineTime = Define.OFFLINE_MAX_TIME;
            offlineTime = Math.Min(offlineTime, maxOfflineTime);
            //Dictionary<int, double> propBoostDic = GameDataMgr.S.GetInventoryData().GetOfflineDic();
            ClaimOfflineReward(offlineTime, Define.OFFLINE_MIN_TIME);

            //GameDataMgr.S.GetInventoryData().ClearOfflineDic();
        }

        private void ClaimOfflineReward(double offlineTime, int minOfflineTime)
        {
            if (offlineTime < minOfflineTime)
            {
                return;
            }

            double offlineReward = m_OfflineRewardStargegy.GetReward(offlineTime);
            if (offlineReward > 0)
            {
               // UIMgr.S.OpenTopPanel(UIID.OfflinePanel, null, offlineReward, offlineTime);
            }
        }
        
        //public int GetOfflineSecondsInInt()
        //{
        //    double offlineTime = GetOfflineSeconds(GameDataMgr.S.GetPlayerInfoData().lastPlayTime);
        //    if (offlineTime > int.MaxValue)
        //    {
        //        return int.MaxValue;
        //    }

        //    return (int)offlineTime;
        //}

        private double GetOfflineSeconds(string lastPlayTime)
        {
            return (DateTime.Now - GetLastPlayDate(lastPlayTime)).TotalSeconds;
        }

        private DateTime GetLastPlayDate(string lastPlayTime)
        {
            string lastTimeStr = lastPlayTime;

            if (lastTimeStr != "0")
            {
                DateTime dtStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                long longTimeLast;
                long.TryParse(lastTimeStr, out longTimeLast);
                DateTime d = dtStart.AddSeconds(longTimeLast);
                return d;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}