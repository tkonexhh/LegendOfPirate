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
        public void SaveManually()
        {
            SetDataDirty();

            //TODO ÔÝÊ±Ê¹ÓÃ
            GameDataMgr.S.SaveDataToLocal();
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
            marketData.refreshTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Define.REFRESH_TIME_POINT, 0, 0);

            SaveManually();
        }

        public DateTime GetRefreshTime()
        {
            return marketData.refreshTime;
        }

        public void ResetRefreshCount()
        {
            marketData.refreshCount = 0;

            SaveManually();
        }

        public void SetRefreshCount()
        {
            marketData.refreshCount = Mathf.Min(TDBlackMarketRefreshConfigTable.GetMaxRefreshID(), marketData.refreshCount + 1);

            SaveManually();
        }

        public int GetRefreshCount()
        {
            return marketData.refreshCount;
        }

        public void AddCommodityDBData(int id, int commodityID, int surplus)
        {
            if (!marketData.commodityDBDatas.Any(i => i.commodityID == commodityID))
                marketData.commodityDBDatas.Add(new CommodityDBData(id, commodityID, surplus));
            else
                Log.e("ID is exit , id = " + commodityID);

            SaveManually();
        }

        public void AddCommodityDBData(CommodityDBData commodityDBData)
        {
            if (!marketData.commodityDBDatas.Any(i => i.commodityID == commodityDBData.commodityID))
                marketData.commodityDBDatas.Add(commodityDBData);
            else
                Log.e("ID is exit , id = " + commodityDBData.commodityID);

            SaveManually();
        }

        public void ReduceCommodityNumber(int commodityID, int number)
        {
            CommodityDBData commodityDBData = marketData.commodityDBDatas.FirstOrDefault(i => i.commodityID == commodityID);
            if (commodityDBData != null)
                commodityDBData.surplus = Mathf.Max(Define.INT_NUMBER_ZERO, commodityDBData.surplus - number);
            else
                Log.e("Commodity not find , id = " + commodityID);

            SaveManually();
        }

        public void ClearAllCommodity()
        {
            marketData.commodityDBDatas.Clear();

            SaveManually();
        }

        public ReactiveCollection<CommodityDBData> GetCommodityDBDatas()
        {
            return new ReactiveCollection<CommodityDBData>(marketData.commodityDBDatas);
        }
        #endregion
    }
}