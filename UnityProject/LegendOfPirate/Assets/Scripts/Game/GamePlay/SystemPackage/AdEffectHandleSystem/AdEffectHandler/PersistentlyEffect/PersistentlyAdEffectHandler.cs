using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    /// <summary>
    /// 有一定时效的广告效果处理
    /// </summary>
	public abstract class PersistentlyAdEffectHandler : ITimeObserver, IAdEffectHandler
    {
        protected AdType m_AdType;

        protected int m_CurTickCount = 0;
        protected int m_PerAddTime = 10;
        protected int m_AdEffectTime = 0;
        protected int m_AdEffectLeftTime = 0;

        protected DateTime m_AdEffectStartTime;

        protected string m_AdEffectStartTimeKey = "AdEffectStartTimeKey";
        protected string m_AdEffectTotalTimeKey = "m_AdEffectTotalTimeKey";

        protected bool m_IsInAdEffect = false;

        #region Public methods
        public PersistentlyAdEffectHandler(AdType adType)
        {
            m_AdType = adType;
        }

        public void Init()
        {
            ResetKey();

            StartAdEffectBySavedData();
        }

        #endregion

        #region IAdEffectHandler
        public void Handle(object[] args)
        {
            if (m_IsInAdEffect)
            {
                m_AdEffectTime += m_PerAddTime;
                m_AdEffectTime = m_AdEffectTime > (int)Define.AD_MAX_TIME ? (int)Define.AD_MAX_TIME : m_AdEffectTime;
                PlayerPrefs.SetInt(m_AdEffectTotalTimeKey, m_AdEffectTime);
                return;
            }

            m_CurTickCount = 0;
            m_AdEffectTime = m_PerAddTime;
            PlayerPrefs.SetInt(m_AdEffectTotalTimeKey, m_AdEffectTime);
            m_AdEffectLeftTime = m_AdEffectTime;
            ResetKey();
            PlayerPrefs.SetString(m_AdEffectStartTimeKey, DateTime.Now.ToString());

            StartAdEffect();
        }

        public AdType GetAdType()
        {
            return m_AdType;
        }

        public int GetLeftTime()
        {
            return m_AdEffectLeftTime;
        }
        #endregion

        #region To be overrided
        protected virtual void StartAdEffect()
        {
            TimeUpdateMgr.S.AddObserver(this);

            m_IsInAdEffect = true;

            EventSystem.S.Send(EventID.OnStartAdEffect, m_AdType);
        }

        protected virtual void EndAdEffect()
        {
            TimeUpdateMgr.S.RemoveObserver(this);

            m_IsInAdEffect = false;

            m_CurTickCount = 0;
            PlayerPrefs.SetString(m_AdEffectStartTimeKey, "");
            PlayerPrefs.SetInt(m_AdEffectTotalTimeKey,0);

            EventSystem.S.Send(EventID.OnEndAdEffect, m_AdType);
        }

        #endregion

        #region Private methods
        private void ResetKey()
        {
            m_AdEffectStartTimeKey = GetType().ToString() + "StartTimeKey";
            m_AdEffectTotalTimeKey = GetType().ToString() + "TotalTimeKey";
        }

        private void StartAdEffectBySavedData()
        {
            string adEffectStartTimeStr = PlayerPrefs.GetString(m_AdEffectStartTimeKey, "");
            if (adEffectStartTimeStr != "")
            {
                m_AdEffectStartTime = DateTime.Parse(adEffectStartTimeStr);
                int deltaTime = (int)((DateTime.Now - m_AdEffectStartTime).TotalSeconds);
                m_AdEffectTime =  PlayerPrefs.GetInt(m_AdEffectTotalTimeKey);
                Log.i("init left time is: " + m_AdEffectLeftTime);
                if (deltaTime > 0 && deltaTime < m_AdEffectTime)
                {
                    m_AdEffectLeftTime = m_AdEffectTime - deltaTime;
                    m_CurTickCount = deltaTime;

                    StartAdEffect();
                }
                else
                {
                    PlayerPrefs.SetString(m_AdEffectStartTimeKey, "");
                    PlayerPrefs.SetInt(m_AdEffectTotalTimeKey, 0);
                }
            }
        }

        private void SetLeftTime()
        {
            m_AdEffectLeftTime = m_AdEffectTime - m_CurTickCount;
            m_AdEffectLeftTime = Mathf.Max(m_AdEffectLeftTime, 0);
        }

        public bool GetIsShowAd
        {
            get
            {
                return m_IsInAdEffect;
            }
        }
        #endregion

        #region TimeObserver
        public int GetTickCount()
        {
            return m_CurTickCount;
        }

        public int GetTickInterval()
        {
            return 1;
        }

        public int GetTotalSeconds()
        {
            return m_AdEffectTime;
        }

        public void OnFinished()
        {
            EndAdEffect();
        }

        public void OnPause()
        {

        }

        public void OnResume()
        {
            //m_CurTickCount += OfflineTimeMgr.S.GetOfflineSecondsInInt();

            //SetLeftTime();
        }

        public void OnStart()
        {

        }

        public virtual void OnTick(int count)
        {
            m_CurTickCount++;
            SetLeftTime();
            //Log.i("Cur tick is: " + m_CurTickCount);
        }

        public bool ShouldRemoveWhenMapChanged()
        {
            return false;
        }

        float IAdEffectHandler.GetLeftTime()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}