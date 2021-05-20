using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class PlayerInfoData : DataDirtyHandler,IResetHandler
    {
        public string coinNumStr;

        public int coinNum;
        public int diamondNum;
        public int starCount;
        public int tipCount;
        public int curLevel = 1;

        public string lastPlayTime;

        public int progressLevel = 5;
        public int progressStar = 1000;

        private double m_CoinNum = 0;

        public void SetDefaultValue()
        {
            curLevel = 1;
            m_CoinNum = Define.DEFAULT_COIN_NUM;
            coinNumStr = m_CoinNum.ToString();

            //countryId = 1;
            coinNum = Define.DEFAULT_COIN_NUM;
            diamondNum = Define.DEFAULT_DIAMOND_NUM;
            tipCount = Define.DEFAULT_TIP_NUM;
            //level = 1;
            //adBoostTime = 0;

            lastPlayTime = "0";
            //isNewPlayer = true;

            SetDataDirty();
        }

        private PlayerInfoTimeObserver m_TimeObserver;

        public void Init()
        {
            m_CoinNum = double.Parse(coinNumStr);

            m_TimeObserver = new PlayerInfoTimeObserver();
            TimeUpdateMgr.S.AddObserver(m_TimeObserver);
        }

        public void SetCoinNum(double num)
        {
            m_CoinNum = num;
            
            EventSystem.S.Send(EventID.OnAddCoinNum);

            SetDataDirty();
        }

        public int GetTipCount()
        {
            return tipCount;
        }

        public double GetCoinNum()
        {
            return m_CoinNum;
        }

        public int GetDiamondNum()
        {
            return diamondNum;
        }

        public int GetStarCount()
        {
            return starCount;
        }

        private float m_LastSendEventTime = 0f;

        public void AddCoinNum(double delta)
        {          
            //if (m_CoinNum < -delta)
            //{
            //    Log.e(m_CoinNum + "/" + delta + "/");
            //}

            m_CoinNum = m_CoinNum + delta;

            if (m_CoinNum < 0)
            {
                m_CoinNum = 0;
            }

            coinNumStr = m_CoinNum.ToString();
            
            EventSystem.S.Send(EventID.OnAddCoinNum);
            
            SetDataDirty();
        }

        public void AddTipCount(int delta)
        {
            tipCount += delta;
            
            SetDataDirty();

            EventSystem.S.Send(EventID.OnTipCountChange);
        }

        public void AddStarCount(int delta)
        {
            starCount += delta;
            if (starCount < 0)
            {
                starCount = 0;
            }

            SetDataDirty();
        }

        public void AddLevel(int delta)
        {
            curLevel += delta;

            SetDataDirty();
        }
        //public void AddShitNum(int delta)
        //{
        //    coinNum += delta;
        //    if (coinNum < 0)
        //    {
        //        coinNum = 0;
        //    }

        //    EventSystem.S.Send(EventID.OnAddDiamondNum);

        //    SetDataDirty();
        //}
        public void AddProgressLevel(int delta)
        {
            progressLevel += delta;
            SetDataDirty();
        }

        public void AddProgressStar(int delta)
        {
            progressStar += delta;
            SetDataDirty();
        }

        public void AddDiamondNum(int delta)
        {
            diamondNum += delta;
            if (diamondNum < 0)
            {
                diamondNum = 0;
            }

            EventSystem.S.Send(EventID.OnAddDiamondNum);

            SetDataDirty();
        }

        public void SetLastPlayTime(string time)
        {
            if (long.Parse(time) > long.Parse(lastPlayTime))
            {
                lastPlayTime = time;
                SetDataDirty();
            }
        }

        public void OnReset()
        {
            m_CoinNum = 0;
            coinNumStr = m_CoinNum.ToString();

            EventSystem.S.Send(EventID.OnAddCoinNum);

            SetDataDirty();
        }
    }
}