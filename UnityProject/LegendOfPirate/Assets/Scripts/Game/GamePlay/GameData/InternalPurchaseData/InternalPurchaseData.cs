using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
    public class InternalPurchaseData : IDataClass
    {
        public ConsumptionData consumptionDatas = new ConsumptionData();

        #region IData
        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }
        #endregion

        #region Public
        public void SetReceiveToday(bool state)
        {
            consumptionDatas.receiveToday = state;

            SetDataDirty();
        }

        public void SetVipState(bool state)
        {
            consumptionDatas.vipState = state;

            SetDataDirty();
        }

        public void SetAutomaticRenewal(bool state)
        {
            consumptionDatas.automaticRenewal = state;

            SetDataDirty();
        }

        public void SetVipPurchaseTime(DateTime dateTime)
        {
            consumptionDatas.vipPurchaseTime = dateTime;

            SetDataDirty();
        }

        public void SetLastCollectionTime(DateTime dateTime)
        {
            consumptionDatas.lastCollectionTime = dateTime;

            SetDataDirty();
        }

        public void SetDeceivedDiamonds(int deceivedDiamonds)
        {
            consumptionDatas.deceivedDiamondsNumber = deceivedDiamonds;

            SetDataDirty();
        }

        public void SetDailyCollectionTimes(int delta = 1)
        {
            consumptionDatas.dailyCollectionTimes = Mathf.Min(InternalPurchaseModel.VIP_MONTH_30, consumptionDatas.dailyCollectionTimes + delta);

            SetDataDirty();
        }

        public void SetFirstCollectionTimes(int delta = 1)
        {
            consumptionDatas.firstCollectionTimes = Mathf.Min(InternalPurchaseModel.VIP_MONTH_30, consumptionDatas.firstCollectionTimes + delta);

            SetDataDirty();
        }

        public void AutomaticRenewalReset()
        {
            consumptionDatas.vipPurchaseTime = default(DateTime);
            consumptionDatas.lastCollectionTime = default(DateTime);
            consumptionDatas.vipState = false;
            consumptionDatas.receiveToday = false;
        }

        public void OnReset()
        {
            AutomaticRenewalReset();
            consumptionDatas.automaticRenewal = false;
            consumptionDatas.deceivedDiamondsNumber = 0;
            consumptionDatas.dailyCollectionTimes = 0;
            consumptionDatas.firstCollectionTimes = 0;
        } 

        public ConsumptionData GetConsumptionData()
        {
            return consumptionDatas;
        }

        #endregion

        #region Private
        #endregion
    }
}