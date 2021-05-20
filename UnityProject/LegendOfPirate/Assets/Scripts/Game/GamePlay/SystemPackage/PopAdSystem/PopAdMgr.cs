using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    /// <summary>
    /// 控制弹出式广告的弹出时机
    /// </summary>
	public class PopAdMgr : TSingleton<PopAdMgr>, IMgr
	{
        private float m_SharedCDTime = 5;
        private float m_SharedCDTimeCounter = 0f;

        private int m_MaxShowAdCount = 2; //限制最多展示的Ad数量
        private int m_NowPopAdCount = 0;
        /// <summary>
        /// 正在走自己CD的AD
        /// </summary>
        private List<PopAdItemBase> m_RunningSelfCDADList = new List<PopAdItemBase>();
        /// <summary>
        /// CD时间已经走完 在等待公共CD 准备展示广告入口的AD
        /// </summary>
        private Queue<PopAdItemBase> m_WaitSharedCDADQueue = new Queue<PopAdItemBase>();

        #region IMgr
        public void Init()
        {
            //初始化动态广告
            //PopAdItemBase PowerUpAd = new PopAdItemBase(AdType.PowerUp, 60, OnSelfCDOver);
            //PowerUpAd.StartCDTimeCounter();

            //PopAdItemBase SummonGiantAd = new PopAdItemBase(AdType.SummonGiant, 40, OnSelfCDOver);
            //SummonGiantAd.StartCDTimeCounter();

            //PopAdItemBase SummonReinforcementsAd = new PopAdItemBase(AdType.SummonReinforcements, 40, OnSelfCDOver);
            //SummonReinforcementsAd.StartCDTimeCounter();

            //PopAdItemBase SupplyAd = new PopAdItemBase(AdType.Supply, 40, OnSelfCDOver);
            //SupplyAd.StartCDTimeCounter();

            //PopAdItemBase SummonPowerUpAd = new PopAdItemBase(AdType.SummonPowerUp, 60, OnSelfCDOver);
            //SummonPowerUpAd.StartCDTimeCounter();


            //m_RunningSelfCDADList.Add(PowerUpAd);
            //m_RunningSelfCDADList.Add(SummonGiantAd);
            //m_RunningSelfCDADList.Add(SummonReinforcementsAd);
            //m_RunningSelfCDADList.Add(SupplyAd);
            //m_RunningSelfCDADList.Add(SummonPowerUpAd);
        }

        public void Update()
        {
            m_SharedCDTimeCounter += Time.deltaTime;
            if (m_SharedCDTimeCounter >= m_SharedCDTime)
            {

                if (m_NowPopAdCount >= m_MaxShowAdCount)
                {
                    //m_SharedCDTimeCounter = 0;
                    return;
                }
                if (m_WaitSharedCDADQueue.Count > 0)
                {
                    PopAdItemBase popAd = m_WaitSharedCDADQueue.Dequeue();
                    popAd.ShowADUI();
                    m_NowPopAdCount++;
                    m_SharedCDTimeCounter = 0f;
                }
            }
        }
        #endregion

        private void OnSelfCDOver(PopAdItemBase ad)
        {
            if (m_RunningSelfCDADList.Contains(ad))
            {
                m_RunningSelfCDADList.Remove(ad);
            }

            if (!m_WaitSharedCDADQueue.Contains(ad))
            {
                m_WaitSharedCDADQueue.Enqueue(ad);
            }
        }

        public void AwakePopAd(AdType adType, int cdtime)
        {
            PopAdItemBase popAdItemBase = new PopAdItemBase(adType, cdtime, OnSelfCDOver);
            popAdItemBase.StartCDTimeCounter();
            m_RunningSelfCDADList.Add(popAdItemBase);

        }

        //
        public void CompleteAdPop()
        {
            m_NowPopAdCount--;
        }







    }

}