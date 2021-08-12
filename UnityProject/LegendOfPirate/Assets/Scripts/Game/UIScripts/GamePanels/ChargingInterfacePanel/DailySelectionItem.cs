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
	public class DailySelectionItem : MonoBehaviour
	{
        #region SerializeField
        [SerializeField] private Button m_DailySelectionItemBtn;
        [SerializeField] private GameObject m_LookAdvState;
        [SerializeField] private GameObject m_PurchaseState;
        [SerializeField] private Toggle m_PurchaseStateTog;
        [SerializeField] private GameObject m_PriceState;
        [SerializeField] private TextMeshProUGUI m_PriceValue;
        [SerializeField] private TextMeshProUGUI m_SelectionItemTitle;
        [SerializeField] private TextMeshProUGUI m_SelectionItemNumber;
        #endregion

        #region Data
        private DailySelectionModel m_DailySelectionModel;
        #endregion

        #region Public

        public void OnInit(DailySelectionModel dailySelectionModel)  
        {
            OnClickAsObservable();

            m_DailySelectionModel = dailySelectionModel;

            BindModelToUI();

            OnRefresh();
        }
        #endregion

        #region Private
        private void OnRefresh()
        {
            //switch (m_DailySelectionModel.dailyDBData.purchaseState)
            //{
            //    case PurchaseState.Purchased:
            //        m_PurchaseStateTog.isOn = true;
            //        break;
            //    case PurchaseState.NotPurchased:
            //        m_PurchaseStateTog.isOn = false;
            //        break;
            //}
        }
        private void OnClickAsObservable()
        {
            m_DailySelectionItemBtn.OnClickAsObservable().Subscribe(_ => HandleDailyBtn()).AddTo(this);
        }

        private void HandleDailyBtn()
        {
            if (m_DailySelectionModel.dailSelectionType.Value == DailSelectionType.Daily)
            {
                switch (m_DailySelectionModel.dailyDBData.purchaseState)
                {
                    case PurchaseState.Purchased:
                        FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.CHARGINGINTERFACE_PURCHASED);
                        break;
                    case PurchaseState.NotPurchased:
                        m_DailySelectionModel.SetDailyPurchaseState();
                        FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.CHARGINGINTERFACE_SUCCESSFULPURCHASED);
                        m_PurchaseStateTog.isOn = true;
                        break;
                }
            }
            else
            {
                //TODO ¿´¹ã¸æ
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.COMMON_SAW_ADV);
            }
        }

        private void BindModelToUI()
        {
            m_DailySelectionModel.priceValue.SubscribeToTextMeshPro(m_PriceValue);
            m_DailySelectionModel.number.SubscribeToTextMeshPro(m_SelectionItemNumber);
            m_DailySelectionModel.title.SubscribeToTextMeshPro(m_SelectionItemTitle);
            m_DailySelectionModel.dailSelectionType.Subscribe(val => { HandleDailyType(val); });
            m_DailySelectionModel.purchasedState.Subscribe(val => { HandlePurchasedType(val); });
        }

        private void HandlePurchasedType(PurchaseState val)
        {
            switch (val)
            {
                case PurchaseState.Purchased:
                    m_PurchaseStateTog.isOn = true;
                    break;
                case PurchaseState.NotPurchased:
                    m_PurchaseStateTog.isOn = false;
                    break;
            }
        }

        private void HandleDailyType(DailSelectionType val)
        {
            switch (val)
            {
                case DailSelectionType.Adv:
                    m_LookAdvState.SetActive(true);
                    m_PurchaseState.SetActive(false);
                    break;
                case DailSelectionType.Daily:
                    m_LookAdvState.SetActive(false);
                    m_PurchaseState.SetActive(true);
                    break;
            }
        }
        #endregion
    }
}