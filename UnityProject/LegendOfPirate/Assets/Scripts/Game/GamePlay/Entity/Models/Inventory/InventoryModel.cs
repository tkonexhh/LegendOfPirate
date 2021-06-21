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

        private Dictionary<InventoryItemType, Dictionary<int, IInventoryItemModel>> m_InventoryItemDics;
        private InventoryData m_InventoryData = null;
        private List<IInventoryItemModel> m_InventoryItemList = new List<IInventoryItemModel>();

        protected override void LoadDataFromDb()
        {
            m_InventoryData = GameDataMgr.S.GetData<InventoryData>();

            for (int i = 0; i < m_InventoryData.itemList.Count; i++)
            {
                InventoryItemType itemType = m_InventoryData.itemList[i].itemType;
                if (!m_InventoryItemDics.ContainsKey(itemType))
                {
                    m_InventoryItemDics.Add(itemType, new Dictionary<int, IInventoryItemModel>());
                }

                if (!m_InventoryItemDics[itemType].ContainsKey(m_InventoryData.itemList[i].id))
                {
                    m_InventoryItemDics[itemType].Add(m_InventoryData.itemList[i].id, InventoryItemModelFactory.CreateItemModel(m_InventoryData.itemList[i], itemType, m_InventoryData.itemList[i].id, m_InventoryData.itemList[i].count));
                }
                else
                {
                    Log.e("Inventory Same Key Added: " + itemType.ToString());
                }
            }
        }

        #region Public Set

        /// <summary>
        /// 改变Item数量，count为正则加，为负则减
        /// </summary>
        public void AddInventoryItemCount(InventoryItemType itemType, int id, int count)
        {
            IInventoryItemModel itemModel = GetItemModel(itemType, id);
            itemModel.AddCount(count);
        }

        #endregion

        #region Public Get

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
            Dictionary<int, IInventoryItemModel> dic = GetDicByItemType(itemType);
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
            {
                return null;
            }
        }

        #endregion

        #region Private

        private Dictionary<int, IInventoryItemModel> GetDicByItemType(InventoryItemType itemType)
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