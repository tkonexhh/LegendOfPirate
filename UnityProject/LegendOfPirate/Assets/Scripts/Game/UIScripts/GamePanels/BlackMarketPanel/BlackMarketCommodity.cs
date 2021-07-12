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
    public class BlackMarketCommodity : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private Button m_MarketCommodityBtn;
        [SerializeField] private TextMeshProUGUI m_CommodityName;
        [SerializeField] private TextMeshProUGUI m_CommodityNumber;
        [SerializeField] private Image m_CommodityIcom;
        [SerializeField, Header("PurchaseState")] private GameObject m_PurchaseState;
        [SerializeField] private Toggle m_PurchaseStateTog;
        [SerializeField, Header("PriceState")] private GameObject m_PriceState;
        [SerializeField] private TextMeshProUGUI m_PriceValue;
        #endregion

        #region Data
        private MarketCommodityMoel m_MarketCommodityMoel;
        #endregion

        #region Public
        public void OnInit(MarketCommodityMoel commodityModel)
        {
            m_MarketCommodityMoel = commodityModel;

            BindModelToUI();

            OnClickAddListener();
        }
        #endregion

        #region Private
        private void OnClickAddListener()
        {
            m_MarketCommodityBtn.OnClickAsObservable().Subscribe(_ => { HandleCommodityBtn(); }).AddTo(this);
        }

        private void HandleCommodityBtn()
        {
            if (m_MarketCommodityMoel.commoditySurplus.Value == 0)
                FloatMessageTMP.S.ShowMsg("Âô¹âÁË");
            else
                UIMgr.S.OpenTopPanel(UIID.CommoditySellPanel, PanelCallback, m_MarketCommodityMoel);
        }

        private void PanelCallback(AbstractPanel obj)
        {
        }

        private void BindModelToUI()
        {
            m_MarketCommodityMoel.commodityName.SubscribeToTextMeshPro(m_CommodityName).AddTo(this);
            m_MarketCommodityMoel.commodityPrice.Select(val => Define.DOLLAR_SIGN + val).SubscribeToTextMeshPro(m_PriceValue).AddTo(this);
            m_MarketCommodityMoel.commodityNumber.SubscribeToTextMeshPro(m_CommodityNumber).AddTo(this);
            m_MarketCommodityMoel.purchaseState.SubscribeToPositiveActive(m_PurchaseState).AddTo(this);
            m_MarketCommodityMoel.purchaseState.SubscribeToNegativeActive(m_PriceState).AddTo(this);
        }
        #endregion
    }
}