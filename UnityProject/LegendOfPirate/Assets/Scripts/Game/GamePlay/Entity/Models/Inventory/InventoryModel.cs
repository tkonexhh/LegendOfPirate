using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class InventoryModel : DbModel
    {
        public IntReactiveProperty level;

        private Dictionary<InventoryItemType, ReactiveDictionary<int, IInventoryItemModel>> m_InventoryItemDics;
        private InventoryData m_InventoryData = null;
        private List<IInventoryItemModel> m_InventoryItemList = new List<IInventoryItemModel>();

        public Dictionary<InventoryItemType, ReactiveDictionary<int, IInventoryItemModel>> InventoryItemDics { get { return m_InventoryItemDics; } }

        protected override void LoadDataFromDb()
        {
            m_InventoryData = GameDataMgr.S.GetData<InventoryData>();
            m_InventoryItemDics = new Dictionary<InventoryItemType, ReactiveDictionary<int, IInventoryItemModel>>();

            for (int i = (int)InventoryItemType.HeroChip; i <= (int)InventoryItemType.Food; i++)
            {
                if (!m_InventoryItemDics.ContainsKey((InventoryItemType)i))
                    m_InventoryItemDics.Add((InventoryItemType)i, new ReactiveDictionary<int, IInventoryItemModel>());
                else
                    Log.e("Inventory Same Key Added: " + (InventoryItemType)i);
            }

            for (int i = 0; i < m_InventoryData.itemList.Count; i++)
            {
                InventoryItemType itemType = m_InventoryData.itemList[i].itemType;

                if (!m_InventoryItemDics[itemType].ContainsKey(m_InventoryData.itemList[i].id))
                    m_InventoryItemDics[itemType].Add(m_InventoryData.itemList[i].id, InventoryItemModelFactory.CreateItemModel(m_InventoryData.itemList[i], itemType, m_InventoryData.itemList[i].id, m_InventoryData.itemList[i].count));
                else
                    Log.e("Inventory Same Key Added: " + itemType.ToString());
            }
        }

        #region Public Set
        /// <summary>
        /// 改变Item数量，count为正则加，为负则减
        /// </summary>
        public void AddInventoryItemCount(InventoryItemType itemType, int id, int count)
        {
            if (CheckItemInInventory(itemType, id))
            {
                IInventoryItemModel itemModel = GetItemModel(itemType, id);
                itemModel.AddCount(count);

                if (itemModel.GetCount() == 0)
                    RemoveInventoryItem(itemModel);
            }
            else
            {
                InventoryItemData newItemData = m_InventoryData.AddNewItemData(itemType, id, count);
                m_InventoryItemDics[itemType].Add(id, InventoryItemModelFactory.CreateItemModel(newItemData, itemType, newItemData.id, newItemData.count));
            }
        }

        public void AddInventoryItemCount(IInventoryItemModel inventoryItemModel, int count)
        {
            AddInventoryItemCount(inventoryItemModel.GetItemType(), inventoryItemModel.GetId(), count);
        }
        #endregion

        #region Public Get
        /// <summary>
        /// 检查某Item是否拥有
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckItemInInventory(InventoryItemType itemType, int id)
        {
            if (m_InventoryItemDics.ContainsKey(itemType))
            {
                if (m_InventoryItemDics[itemType].ContainsKey(id))
                    return true;
                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 获取所有的Item
        /// </summary>
        public List<IInventoryItemModel> GetItemModelList()
        {
            m_InventoryItemList.Clear();
            foreach (var dic in m_InventoryItemDics.Values)
            {
                foreach (var item in dic.Values)
                {
                    m_InventoryItemList.Add(item);
                }
            }
            return m_InventoryItemList;
        }

        public IInventoryItemModel GetItemModel(InventoryItemType itemType, int id)
        {
            ReactiveDictionary<int, IInventoryItemModel> dic = GetDicByItemType(itemType);
            if (dic != null)
            {
                if (dic.ContainsKey(id))
                {
                    return dic[id];
                }
                else
                {
                    Log.e("Inventory Item Id Not Found In Dic: " + itemType.ToString() + " id: " + id);

                    return null;
                }
            }
            else
                return null;
        }

        #endregion

        #region Private
        private void RemoveInventoryItem(IInventoryItemModel inventoryItemModel)
        {
            if (m_InventoryItemDics.ContainsKey(inventoryItemModel.GetItemType()))
            {
                m_InventoryItemDics[inventoryItemModel.GetItemType()].Remove(inventoryItemModel.GetId());

                m_InventoryData.RemoveItemData(inventoryItemModel);

                return;
            }

            Log.e("Not Fint Item,ItemType = " + inventoryItemModel.GetItemType());
        }

        private ReactiveDictionary<int, IInventoryItemModel> GetDicByItemType(InventoryItemType itemType)
        {
            if (m_InventoryItemDics.ContainsKey(itemType))
            {
                return m_InventoryItemDics[itemType];
            }
            else
            {
                Log.e("Key Not Found In Inventroy Dic : " + itemType.ToString());

                return null;
            }
        }
        #endregion
    }
}