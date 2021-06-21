
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class WareHouseItem : IUListItemView
    {
        #region SerializeField
        [SerializeField] private Button m_WareHouseItemBtn;
        #endregion

        #region Data
        private WareHouseItemModel m_WareHouseItemModel;
        private IntReactiveProperty m_ItemCount;
        private List<IDisposable> m_IDisItemCounts;
        #endregion

        #region Method
        private void OnReset()
        {
            if (m_IDisItemCounts != null)
            {
                foreach (var item in m_IDisItemCounts)
                    item.Dispose();
                m_IDisItemCounts.Clear();
            }
            else
                m_IDisItemCounts = new List<IDisposable>();
            m_Full.enabled = false;
        }

        public void OnRefresh(WareHouseItemModel wareHouseItemModel)
        {
            if (wareHouseItemModel == null)
            {
                Debug.LogError("wareHouseItemModel is null");
                return;
            }

            m_WareHouseItemModel = wareHouseItemModel;
            m_ItemCount = m_WareHouseItemModel.inventoryItemModel.GetReactiveCount();

            BindModelToUI();

            OnClickAddListener();
        }

        private void OnClickAddListener()
        {
            m_WareHouseItemBtn
                .OnClickAsObservable()
                .Subscribe(_ => { OpenItemDetailsPanel(); })
                .AddTo(this);
        }

        private void OpenItemDetailsPanel()
        {
            UIMgr.S.OpenTopPanel(UIID.ItemDetailsPanel, PanelCallback, m_WareHouseItemModel.inventoryItemModel);
        }

        private void PanelCallback(AbstractPanel obj)
        {
        }

        private void BindModelToUI()
        {
            OnReset();
            if (m_ItemCount != null)
            {
                IDisposable number = m_ItemCount.SubscribeToTextMeshPro(m_Number).AddTo(this);
                IDisposable full = m_ItemCount
                    .Select(count => count >= Define.INVENTORY_ITEM_MAX_COUNT ? true : false)
                    .Subscribe(val => { m_Full.enabled = val; }).AddTo(this);
                m_IDisItemCounts.Add(number);
                m_IDisItemCounts.Add(full);
            }
        }
        #endregion
    }
}