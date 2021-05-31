using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public partial class InventoryMgr : TSingleton<InventoryMgr>, IMgr
    {
        private Dictionary<int, IInventoryItem> m_InventoryItemDic;

        #region IMgr
        public void OnInit()
        {
            m_InventoryItemDic = new Dictionary<int, IInventoryItem>();

            LoadDataFromModel();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_InventoryItemDic = null;
        }

        #endregion

        #region Public

        public void AddInventoryItem<T>(T id, IInventoryItem item) where T : IConvertible
        {
            int key = id.ToInt32(null);

            IInventoryItem savedItem = GetItem<T>(id);

            if (savedItem == null)
            {
                m_InventoryItemDic.Add(key, item);
            }
            else
            {
                m_InventoryItemDic[key].AddCount(item.GetCount());
            }
        }

        public bool RemoveInventoryItem<T>(T id) where T : IConvertible
        {
            int key = id.ToInt32(null);

            IInventoryItem savedItem = GetItem<T>(id);

            if (savedItem == null)
            {
                Log.e("Inventory item not found: " + id.ToString());

                return false;
            }
            else
            {
                m_InventoryItemDic.Remove(key);

                return true;
            }
        }

        public bool SubInventoryItemCount<T>(T id, int count) where T : IConvertible
        {
            int key = id.ToInt32(null);

            IInventoryItem savedItem = GetItem<T>(id);

            if (savedItem == null)
            {
                Log.e("Inventory item not found: " + id.ToString());

                return false;
            }
            else
            {
                int savedItemCount = savedItem.GetCount();
                if (savedItemCount >= count)
                {
                    savedItem.SubCount(count);
                    return true;
                }
                else
                {
                    Log.e("Inventory item count not enough: " + id.ToString() + " count：" + savedItemCount);
                    return false;
                }
            }
        }

        public IInventoryItem GetItem<T>(T id) where T : IConvertible
        {
            int key = id.ToInt32(null);
            if (m_InventoryItemDic.ContainsKey(key))
            {
                return m_InventoryItemDic[key];
            }

            return null;
        }
        #endregion

        #region Private

        #endregion
    }

}