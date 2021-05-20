using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class PopAdItemBase : ITimeObserver
    {
        private int m_TickCount;
        private int m_CDTime;
        private AdType m_AdType;

        private Action<PopAdItemBase> m_OnCDTimeOverCallback = null;

        public PopAdItemBase(AdType adType, int cdTime, Action<PopAdItemBase> cdTimeOverCallback)
        {
            m_AdType = adType;
            m_CDTime = cdTime;

            m_OnCDTimeOverCallback = cdTimeOverCallback;
        }

        #region ITimeObserver
        public int GetTickCount()
        {
            return m_TickCount;
        }

        public int GetTickInterval()
        {
            return 1;
        }

        public int GetTotalSeconds()
        {
            return m_CDTime;
        }

        public void OnFinished()
        {
            m_TickCount = 0;

            OnCDTimeOver();
        }

        public void OnPause()
        {

        }

        public void OnResume()
        {
            //m_TickCount += OfflineTimeMgr.S.GetOfflineSecondsInInt();
        }

        public void OnStart()
        {

        }

        public void OnTick(int count)
        {
            m_TickCount++;
        }

        public bool ShouldRemoveWhenMapChanged()
        {
            return false;
        }
        #endregion

        #region To be overrided
        protected virtual void OnCDTimeOver()
        {
            if (m_OnCDTimeOverCallback != null)
            {
                m_OnCDTimeOverCallback.Invoke(this);
            }
            Log.i("Pop ad cd time over: " + m_AdType.ToString() + " Time: " + Time.time);
        }

        /// <summary>
        /// 自身CD已经走完 公共CD也已经走完，可以展示UI上面的广告点击入口了
        /// </summary>
        public virtual void ShowADUI()
        {
            EventSystem.S.Send(EventID.OnShowPopAdUI, m_AdType);
            Log.i("Pop ad show ad ui: " + m_AdType.ToString() + " Time: " + Time.time);
        }
        #endregion

        #region Public Methods
        public void SetTotalCDTime(int total)
        {
            m_CDTime = total;
        }

        /// <summary>
        /// 开始CD倒计时
        /// </summary>
        /// <param name="startTickCount"></param>
        public void StartCDTimeCounter(int startTickCount = 0)
        {
            Log.i("Pop ad start counter: " + m_AdType.ToString() + " Time:" + Time.time);
            m_TickCount = startTickCount;
            AddToTimeObservers();
        }
        #endregion

        #region Private Methods

        private void AddToTimeObservers()
        {
            TimeUpdateMgr.S.AddObserver(this);
        }

        #endregion
    }

}