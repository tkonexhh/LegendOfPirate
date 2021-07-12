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
    public class MarketCommodityMoel
    {
        private CommodityDBData commodityDBData;
        private BlackMarketConfig blackMarketConfig;
        private BlackMarketCommodity blackMarketCommodity;

        public StringReactiveProperty commodityName;
        public IntReactiveProperty commodityPrice;
        public IntReactiveProperty commoditySurplus;
        public IReadOnlyReactiveProperty<bool> purchaseState;
        public IReadOnlyReactiveProperty<string> commodityNumber;

        public MarketCommodityMoel(CommodityDBData commodityDBData)
        {
            this.commodityDBData = commodityDBData;
            blackMarketConfig = TDBlackMarketConfigTable.GetBlackMarketConfig(this.commodityDBData.id);

            commodityName = new StringReactiveProperty(blackMarketConfig.name);
            commodityPrice = new IntReactiveProperty(blackMarketConfig.price);
            commoditySurplus = new IntReactiveProperty(this.commodityDBData.surplus);
            purchaseState = commoditySurplus.Select(val => val == 0).ToReactiveProperty();
            commodityNumber = commoditySurplus.Select(val => val + Define.SYMBOL_SLASH + blackMarketConfig.marketConfigItem.count).ToReactiveProperty();
        }

        #region Public
        public void OnRefresh(CommodityDBData commodityDBData)
        {
            this.commodityDBData = commodityDBData;
            blackMarketConfig = TDBlackMarketConfigTable.GetBlackMarketConfig(this.commodityDBData.id);

            commodityName.Value = blackMarketConfig.name;
            commodityPrice.Value = blackMarketConfig.price;
            commoditySurplus.Value = this.commodityDBData.surplus;
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="number"></param>
        public void PurchaseCommodity(int number)
        {
            // TODO 判定钱够不够
            //ModelMgr.S.GetModel<PlayerInfoData>().AddDiamond(-number * commodityPrice.Value);

            ModelMgr.S.GetModel<BlackMarketModel>().PurchaseCommodity(commodityDBData.commodityID, number);

            commoditySurplus.Value = Mathf.Max(Define.INT_NUMBER_ZERO, commoditySurplus.Value - number);
        }

        public void SetDataModel(BlackMarketCommodity blackMarketCommodity)
        {
            this.blackMarketCommodity = blackMarketCommodity;
        }
        #endregion
    }
}