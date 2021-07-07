using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using System.Linq;
using UniRx;
using System;

namespace GameWish.Game
{
    public class BlackMarketData : IDataClass
    {
        public MarketData marketData = new MarketData();

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
        public void SetRefreshTime()
        {
            if (DateTime.Now.Hour < Define.REFRESH_TIME_POINT)
                marketData.refreshTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, Define.REFRESH_TIME_POINT, 0, 0);
            else
                marketData.refreshTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Define.REFRESH_TIME_POINT, 0, 0);
        }

        public DateTime GetRefreshTime()
        {
            return marketData.refreshTime;
        }

        public void ResetRefreshCount()
        {
            marketData.refreshCount = 0;
        }

        public void SetRefreshCount()
        {
            marketData.refreshCount = Mathf.Min(TDBlackMarketRefreshConfigTable.GetMaxRefreshID(), marketData.refreshCount + 1);
        }

        public int GetRefreshCount()
        {
            return marketData.refreshCount;
        }

        public void AddCommodityDBData(int id, int commodityID, int surplus)
        {
            if (!marketData.commodityDBDatas.Any(i => i.commodityID == id))
                marketData.commodityDBDatas.Add(new CommodityDBData(id, commodityID, surplus));
            else
                Log.e("ID is exit , id = " + id);
        }

        public void ReduceCommodityNumber(int commodityID, int number)
        {
            CommodityDBData commodityDBData = marketData.commodityDBDatas.FirstOrDefault(i => i.commodityID == commodityID);
            if (commodityDBData != null)
                commodityDBData.surplus = Mathf.Max(Define.NUMBER_ZERO, commodityDBData.surplus - number);
            else
                Log.e("Commodity not find , id = " + commodityID);
        }

        public void ClearAllCommodity()
        {
            marketData.commodityDBDatas.Clear();
        }

        public ReactiveCollection<CommodityDBData> GetCommodityDBDatas()
        {
            return new ReactiveCollection<CommodityDBData>(marketData.commodityDBDatas);
        }
        #endregion
    }
}