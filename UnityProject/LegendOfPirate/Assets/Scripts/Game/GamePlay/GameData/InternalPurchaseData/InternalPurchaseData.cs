using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;
using System.Linq;

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
        public ConsumptionData GetConsumptionData()
        {
            return consumptionDatas;
        }

        #region Vip
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

            SetDataDirty();
        }

        public void OnReset()
        {
            AutomaticRenewalReset();
            consumptionDatas.automaticRenewal = false;
            consumptionDatas.deceivedDiamondsNumber = 0;
            consumptionDatas.dailyCollectionTimes = 0;
            consumptionDatas.firstCollectionTimes = 0;

            SetDataDirty();
        }
        #endregion

        #region Daily Selection

        public ReactiveCollection<DailyDBData> GetDailyDataModels()
        {
            return new ReactiveCollection<DailyDBData>(consumptionDatas.dailyDataModels);
        }

        public void SetDailyInitialTime()
        {
            consumptionDatas.dailyInitialTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
        }

        public void SetDailyPurchaseState(int id, PurchaseState purchaseState)
        {
            DailyDBData dailyDBData = consumptionDatas.dailyDataModels.FirstOrDefault(i => i.id == id);
            if (dailyDBData != null)
                dailyDBData.purchaseState = purchaseState;
            else
                Log.e("Not find id = " + id);

            SetDataDirty();
        }

        public void ClearAllDailyID()
        {
            consumptionDatas.dailyDataModels.Clear();
        }

        #endregion

        #endregion

        #region Private
        #endregion
    }
}