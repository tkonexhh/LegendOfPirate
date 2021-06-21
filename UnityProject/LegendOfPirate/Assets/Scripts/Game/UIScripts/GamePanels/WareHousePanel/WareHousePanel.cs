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
        public const InventoryItemType DefaultOpenItemType = InventoryItemType.Equip;
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

            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Equip, 1, 1);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Equip, 2, 1);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Equip, 3, 1);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Equip, 4, 1);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Food, 4, 5);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.HeroChip, 4, 2);
            m_PanelData.inventoryModel.AddInventoryItemCount(InventoryItemType.Material, 4, 23);

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
        #region Other Data
        private void InitData()
        {
            InitItemTypeTog();

            OnRefreshInventoryList(DefaultOpenItemType);

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

            wareHouseItemModel.SetWareHouseItemData(wareHouseItem);

            wareHouseItem.OnRefresh(wareHouseItemModel);
        }

        private void OnRefreshInventoryList(InventoryItemType inventoryItemType)
        {
            m_WareHouseItemModels.Clear();
            foreach (var item in m_PanelData.inventoryModel.GetItemModelList())
            {
                if (item.GetItemType() == inventoryItemType)
                {
                    m_WareHouseItemModels.Add(new WareHouseItemModel(item));
                }
            }
        }
        #endregion
    }
}
