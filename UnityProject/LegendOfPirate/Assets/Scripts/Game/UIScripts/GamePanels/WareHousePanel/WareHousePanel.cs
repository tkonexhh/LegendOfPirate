using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class WareHousePanel : AbstractAnimPanel
    {
        #region SerializeField
        [SerializeField]
        private GameObject m_ItemTypeTog;   
        [SerializeField]
        private Transform m_ItemTogGroup;
        [SerializeField]
        private UGridListView m_WareHouseUGList;
        #endregion

        #region Data
        public InventoryItemType openItemType = InventoryItemType.Equip;
        //private List<IInventoryItemModel> m_InventoryItemList = new List<IInventoryItemModel>();
        private List<WareHouseItemModel> m_WareHouseItemModels = new List<WareHouseItemModel>();

        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            BindModelToUI();

            BindUIToModel();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            InitData();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
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
        }
        #endregion

        #region Other Method
        private void InitData()
        {
            InitItemTypeTog();

            OnRefreshInventoryList(openItemType);

            m_WareHouseUGList.SetCellRenderer(OnWareHouseCellRenderer);
            m_WareHouseUGList.SetDataCount(m_WareHouseItemModels.Count);
        }

        private void InitItemTypeTog()
        {
            for (int i = (int)InventoryItemType.HeroChip; i <= (int)InventoryItemType.Food; i++)
            {
                ItemTypeTog itemTypeTog = Instantiate(m_ItemTypeTog, m_ItemTogGroup).GetComponent<ItemTypeTog>();
                itemTypeTog.OnInit((InventoryItemType)i,this);
            }
        }

        public void OnRefreshItemListByType(InventoryItemType inventoryItemType)
        {
            OnRefreshInventoryList(inventoryItemType);
            m_WareHouseUGList.SetDataCount(m_WareHouseItemModels.Count);
        }

        private void OnWareHouseCellRenderer(Transform root, int index)
        {
            WareHouseItemModel wareHouseItemModel = m_WareHouseItemModels[index];

            WareHouseItem wareHouseItem = root.GetComponent<WareHouseItem>();

            wareHouseItemModel.SetWareHouseItemData(wareHouseItem,this);

            wareHouseItem.OnRefresh(wareHouseItemModel);
        }

        public void OnRefreshInventoryList(InventoryItemType inventoryItemType)
        {
            openItemType = inventoryItemType;
            m_WareHouseItemModels.Clear();

            foreach (var item in m_PanelData.inventoryModel.InventoryItemDics[inventoryItemType].Values)
            {
                m_WareHouseItemModels.Add(new WareHouseItemModel(item));
            }
        }
        #endregion
    }
}
