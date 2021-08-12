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
	public class PurchaseDiamondsItem : MonoBehaviour
	{
        #region SerializeField
        [SerializeField] private Button m_DiamondsItemBtn;
        [SerializeField] private Image m_BuyDiamondsIcon;
        [SerializeField] GameObject  m_MultipleBg;
        [SerializeField] TextMeshProUGUI m_PurchaseDiamondsItemTitle;
        [SerializeField] TextMeshProUGUI m_BuyDiamondsPrice;
        #endregion


        #region Data
        private DiamondsRegionModel m_DiamondsRegionModel;
        #endregion

        #region Public
        public void OnInit(DiamondsRegionModel diamondsRegionModel)
        {
            m_DiamondsRegionModel = diamondsRegionModel;

            OnClickAsObservable();

            BindModelToUI();
        }
        #endregion
        #region Private
        private void OnClickAsObservable()
        {
            m_DiamondsItemBtn.OnClickAsObservable().Subscribe(_=> HandleBuyDiamonds()).AddTo(this);
        }

        private void HandleBuyDiamonds()
        {
            ModelMgr.S.GetModel<PlayerInfoData>()?.AddDiamond(m_DiamondsRegionModel.giftNumber.Value);
            FloatMessageTMP.S.ShowMsg("购买成功,数量+"+ m_DiamondsRegionModel.giftNumber.Value);
        }

        private void BindModelToUI()
        {
            m_DiamondsRegionModel.giftName.SubscribeToTextMeshPro(m_PurchaseDiamondsItemTitle).AddTo(this);
            m_DiamondsRegionModel.giftPrice.SubscribeToTextMeshPro(m_BuyDiamondsPrice).AddTo(this);
        }
        #endregion
    }
}