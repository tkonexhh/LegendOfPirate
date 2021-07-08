using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class CommoditySellPanel : AbstractAnimPanel
    {
        #region Data
        private MarketCommodityMoel m_MarketCommodityMoel;
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(1);
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

            HandleTransmitValue(args);

            BindModelToUI();
            BindUIToModel();

            InitData();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();

            CloseDependPanel(EngineUI.MaskPanel);
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            m_ExitBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); }).AddTo(this);
            m_ReduceBtn.OnClickAsObservable().Subscribe(_ => { ReduceBtnEvt(); }).AddTo(this);
            m_IncreaseBtn.OnClickAsObservable().Subscribe(_ => { IncreaseBtnEvt(); }).AddTo(this);
            m_MaxBtn.OnClickAsObservable().Subscribe(_ => { MaxBtnEvt(); }).AddTo(this);
            m_MinBtn.OnClickAsObservable().Subscribe(_ => { MinBtnEvt(); }).AddTo(this);
        }

        private void ReduceBtnEvt()
        {
            m_SelectedCount.Value = Mathf.Max(1, m_SelectedCount.Value - 1);
        }

        private void IncreaseBtnEvt()
        {
            if (m_MarketCommodityMoel.IsNotNull())
            {
                m_SelectedCount.Value = Mathf.Min(m_MarketCommodityMoel.commoditySurplus.Value, m_SelectedCount.Value + 1);
            }
            else
                Debug.LogError("Commodity is null");
        } 
        
        private void HandleSellEvt()
        {
            //if (true)//TODO ×êÊ¯²»¹»
            //{
            //    UIMgr.S.OpenPanel(UIID.DiamondShortagePanel);
            //}
            HideSelfWithAnim();
            m_MarketCommodityMoel.PurchaseCommodity(m_SelectedCount.Value);
        }

        private void MaxBtnEvt()
        {
            if (m_MarketCommodityMoel.IsNotNull())
            {
                m_SelectedCount.Value = m_MarketCommodityMoel.commoditySurplus.Value;
            }
            else
                Debug.LogError("Commodity is null;");
        }

        private void MinBtnEvt()
        {
            if (m_MarketCommodityMoel.IsNotNull())
            {
                m_SelectedCount.Value = 1;
            }
            else
                Debug.LogError("Commodity is null;");
        }

        private void HandleMaxAndMinBtnActive(int val)
        {
            if (m_MarketCommodityMoel.IsNotNull())
            {
                if (val == 1)
                {
                    m_MinBtn.gameObject.SetActive(false);
                    m_MaxBtn.gameObject.SetActive(true);
                }
                else if (val == m_MarketCommodityMoel.commoditySurplus.Value)
                {
                    m_MaxBtn.gameObject.SetActive(false);
                    m_MinBtn.gameObject.SetActive(true);
                }
                else
                {
                    m_MaxBtn.gameObject.SetActive(true);
                    m_MinBtn.gameObject.SetActive(true);
                }
            }
            else
                Log.e("Commodity is null;");
        }
        #endregion

        #region Private
        private void HandleTransmitValue(params object[] args)
        {
            m_MarketCommodityMoel = args[0] as MarketCommodityMoel;
        }

        private void InitData()
        {
            if (m_MarketCommodityMoel.commoditySurplus.Value == 0)
                m_SelectedCount.Value = 0;
        }
        #endregion
    }
}
