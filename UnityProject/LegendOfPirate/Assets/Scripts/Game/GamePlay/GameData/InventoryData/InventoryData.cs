using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class InventoryData : IDataClass
    {
        public List<InventoryItemData> itemList = new List<InventoryItemData>();

        #region IData
        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }
        #endregion

        #region Public

        /// <summary>
        /// 增加一个新的物品（可空值类型?待测试）
        /// </summary>
        /// <param name="inventoryItemType"></param>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public InventoryItemData AddNewItemData(InventoryItemType inventoryItemType, int id, int count)
        {
            if (!CheckItemDataInventory(inventoryItemType, id))
            {
                InventoryItemData createResut = new InventoryItemData(inventoryItemType, id, count);
                itemList.Add(createResut);
                SetDataDirty();
                return createResut;
            }
            else
            {
                Log.e("ItemType不应该存在，ItemType =  " + inventoryItemType);
                return itemList.FirstOrDefault(i => i.itemType == inventoryItemType && i.id == id);
            }
        }

        public void RemoveItemData(IInventoryItemModel inventoryItemModel)
        {
            if (CheckItemDataInventory(inventoryItemModel.GetItemType(), inventoryItemModel.GetId()))
            {
                InventoryItemData inventoryItemData = itemList.FirstOrDefault(val => val.itemType == inventoryItemModel.GetItemType() && val.id == inventoryItemModel.GetId());
                itemList.Remove(inventoryItemData);
            }
            else
            {
                Log.e("ItemType应该存在，ItemType = " + inventoryItemModel);
            }
        }
        #endregion
        #region Private

        /// <summary>
        /// 检查Item是否存在 
        /// </summary>
        /// <param name="inventoryItemType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CheckItemDataInventory(InventoryItemType inventoryItemType, int id)
        {
            bool checkState = false;

            foreach (var item in itemList)
            {
                if (item.itemType == inventoryItemType && item.id == id)
                {
                    checkState = true;
                    break;
                }
            }
            return checkState;
        }
        #endregion
    }
}