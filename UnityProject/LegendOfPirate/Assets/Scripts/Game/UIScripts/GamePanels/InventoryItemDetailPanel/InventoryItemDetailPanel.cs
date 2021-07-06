using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public partial class InventoryItemDetailPanel : AbstractAnimPanel
    {
        #region Data
        private IInventoryItemModel m_InventoryItemModel;
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
            if (m_InventoryItemModel.IsNotNull())
            {
                m_SelectedCount.Value = Mathf.Min(m_InventoryItemModel.GetCount(), m_SelectedCount.Value + 1);
            }
            else
                Debug.LogError("IInventoryItemModel is null; ItemType" + m_InventoryItemModel.GetItemType());
        }
        private void MaxBtnEvt()
        {
            if (m_InventoryItemModel.IsNotNull())
            {
                m_SelectedCount.Value = m_InventoryItemModel.GetCount();
            }
            else
                Debug.LogError("IInventoryItemModel is null;");
        }
        private void MinBtnEvt()
        {
            if (m_InventoryItemModel.IsNotNull())
            {
                m_SelectedCount.Value = 1;
            }
            else
                Debug.LogError("IInventoryItemModel is null;");

        }
        private void HandleSellBtnEvt()
        {
            HideSelfWithAnim();
            m_PanelData.inventoryModel.AddInventoryItemCount(m_InventoryItemModel, -m_SelectedCount.Value);
            //TODO ¼ÓÇ®
        }
        private void HandleMaxAndMinBtnActive(int val)
        {
            if (m_InventoryItemModel.IsNotNull())
            {
                if (val == 1)
                {
                    m_MinBtn.gameObject.SetActive(false);
                    m_MaxBtn.gameObject.SetActive(true);
                }
                else if (val == m_InventoryItemModel.GetCount())
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
                Log.e("IInventoryItemModel is null;");
        }
        #endregion

        #region Other Method
        private void HandleTransmitValue(params object[] args)
        {
            m_InventoryItemModel = args[0] as IInventoryItemModel;
        }
        private void InitData()
        {
            if (m_InventoryItemModel != null)
            {
                //m_ItemName.text
                //m_ItemDesc;
                //m_ItemIcon;
                //m_UnitPriceValue = inventoryItemModel
            }
        }
        #endregion
    }
}
