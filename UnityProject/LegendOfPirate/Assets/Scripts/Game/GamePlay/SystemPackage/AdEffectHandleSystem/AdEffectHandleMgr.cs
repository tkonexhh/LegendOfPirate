using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class AdEffectHandleMgr : TSingleton<AdEffectHandleMgr>, IMgr
    {
        private List<IAdEffectHandler> m_AdHandlerList = new List<IAdEffectHandler>();

        #region IMgr
        public void Init()
        {
            InitAllAdHander();
        }

        public void Update()
        {
        }
        #endregion

        #region Public methods

        public void Handle(AdType adType, object[] args)
        {
            IAdEffectHandler adHandler = GetAdHandler(adType);
            if (adHandler != null)
            {
                adHandler.Handle(args);
            }
            else
            {
                Log.e("Ad handler can't be found: " + adType.ToString());
            }
        }

        public float GetLeftTime(AdType adType)
        {
            IAdEffectHandler adHandler = GetAdHandler(adType);
            if (adHandler != null)
            {
                return adHandler.GetLeftTime();
            }
            else
            {
                Log.e("Ad handler can't be found: " + adType.ToString());
            }

            return -1;
        }

        public IAdEffectHandler GetAdHandler(AdType adType)
        {
            foreach (IAdEffectHandler adHandler in m_AdHandlerList)
            {
                if (adHandler.GetAdType() == adType)
                {
                    return adHandler;
                }
            }

            return null;
        }

        #endregion

        #region Private methods

        private void InitAllAdHander()
        {
            // 初始化所有PersistlyAdHandler, 注意需要Init
            //DoubleIncomeAdHandler doubleIncomeAdHandler = new DoubleIncomeAdHandler(AdType.DoubleIncome);
            //doubleIncomeAdHandler.Init();
            //m_AdHandlerList.Add(doubleIncomeAdHandler);

            //PowerUpAdHandler powerUpAdHandler = new PowerUpAdHandler(AdType.PowerUp);
            //powerUpAdHandler.Init();
            //m_AdHandlerList.Add(powerUpAdHandler);

            //SummonPowerUpAdHandler summonPowerUpAdHandler = new SummonPowerUpAdHandler(AdType.SummonPowerUp);
            //summonPowerUpAdHandler.Init();
            //m_AdHandlerList.Add(summonPowerUpAdHandler);

            //自动双倍
            //DoubleSummonAdHandler doubleSummonAdHandler = new DoubleSummonAdHandler(AdType.AutoDoubleSummon);
            //doubleSummonAdHandler.Init();
            //m_AdHandlerList.Add(doubleSummonAdHandler);


            //// 初始化所有InstantlyAdHandler
            //InstantCoinAdHandler instantCoinAdHandler = new InstantCoinAdHandler(AdType.InstantCoin);
            //InstantCoinAdHandler instantCoinAdHandler2 = new InstantCoinAdHandler(AdType.Supply);
           
            //InstantMethodAdHandler instantMethodAdHandler = new InstantMethodAdHandler(AdType.SummonGiant);
            //InstantMethodAdHandler instantMethodAdHandler1 = new InstantMethodAdHandler(AdType.SummonReinforcements);

            //m_AdHandlerList.Add(instantCoinAdHandler);
            //m_AdHandlerList.Add(instantCoinAdHandler2);
            //m_AdHandlerList.Add(instantMethodAdHandler);
            //m_AdHandlerList.Add(instantMethodAdHandler1);
        }

        #endregion
    }

}