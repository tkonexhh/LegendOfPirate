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
    public class DailySelectionModel
    {
        public DailyDBData dailyDBData;
        public DailySelectionItem dailySelectionItem;
        public DailySelectionConfig dailySelectionConfig;

        #region ReactiveProperty
        public FloatReactiveProperty priceValue;
        public FloatReactiveProperty number;
        public StringReactiveProperty title;
        public ReactiveProperty<DailSelectionType> dailSelectionType;
        public ReactiveProperty<PurchaseState> purchasedState;
        #endregion
        #region Public
        public DailySelectionModel(DailyDBData dailyDBData)
        {
            this.dailyDBData = dailyDBData;
            dailySelectionConfig = TDDailySelectionConfigTable.GetDailySelectionConfig(this.dailyDBData.id);

            priceValue = new FloatReactiveProperty(dailySelectionConfig.price);
            number = new FloatReactiveProperty(dailySelectionConfig.number);
            title = new StringReactiveProperty(dailySelectionConfig.itemName);
            dailSelectionType = new ReactiveProperty<DailSelectionType>(dailyDBData.dailSelectionType);
            purchasedState = new ReactiveProperty<PurchaseState>(dailyDBData.purchaseState);
        }

        public void SetModelData(DailySelectionItem dailySelectionItem)
        {
            this.dailySelectionItem = dailySelectionItem;
        }

        public void SetDailyPurchaseState()
        {
            purchasedState.Value = PurchaseState.Purchased;
            ModelMgr.S.GetModel<InternalPurchaseModel>().SetDailyPurchaseState(dailyDBData.id);
        }

        public void OnRefresh(DailyDBData dailyDBData)
        {
            this.dailyDBData = dailyDBData;
            dailySelectionConfig = TDDailySelectionConfigTable.GetDailySelectionConfig(this.dailyDBData.id);
            priceValue.Value = dailySelectionConfig.price;
            number.Value = dailySelectionConfig.number;
            title.Value = dailySelectionConfig.itemName;
            dailSelectionType.Value = dailyDBData.dailSelectionType;
            purchasedState.Value = dailyDBData.purchaseState;
        }
        #endregion
    }
}