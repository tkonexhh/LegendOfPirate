using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    public abstract class InventoryItemModel : Model, IInventoryItemModel
    {
        protected InventoryItemData m_DbItem;
        protected InventoryItemType m_ItemType;
        protected IntReactiveProperty m_Count;
        protected int m_Id;

        public InventoryItemModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count)
        {
            m_DbItem = dbItem;
            m_ItemType = itemType;
            m_Id = id;
            m_Count = new IntReactiveProperty(count);

            m_Count.Where(val=> val > 0).Subscribe((value) => { m_DbItem.OnValueChanged(value); });
        }

        #region IInventoryItemModel

        public void AddCount(int deltaCount)
        {
            int value = m_Count.Value + deltaCount;
            value = Mathf.Min(Define.INVENTORY_ITEM_MAX_COUNT,Mathf.Max(0, value));

            m_Count.Value = value;
        }

        public int GetCount()
        {
            return m_Count.Value;
        }

        public InventoryItemType GetItemType()
        {
            return m_ItemType;
        }
        public IntReactiveProperty GetReactiveCount()
        {
            return m_Count;
        }

        public int GetId()
        {
            return m_Id;
        }

        public abstract string GetDesc();

        public abstract string GetName();
        #endregion

    }

}