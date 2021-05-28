using Qarth;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameWish.Game
{
	public class InventroyMgr : TSingleton<InventroyMgr>, IMgr
	{
        private InventroyFactory m_InventroyFactory = new InventroyFactory();

        private List<ItemBase> m_InventroyList = new List<ItemBase>();

        /// <summary>
        /// 单体最大数量
        /// </summary>
        public const long MAXITEMNUMBER = 20;
        #region IMgr
        public void OnDestroyed()
        {
        }

        public void OnInit()
        {
            //AddItemForID(ItemType.Consumable, ItemID._1, 1);
            //AddItemForID(ItemType.Consumable, ItemID._1, 30);
            //DeleteItemForID(ItemID._1, 20);
        }

        public void OnUpdate()
        {
        }

        #endregion

        #region Private Mehtod
        private void HandleSurplusItemNumber(ItemType itemType, ItemID id, long number)
        {
            long heap = number / MAXITEMNUMBER;
            long heapRemainder = number % MAXITEMNUMBER;
            for (int i = 0; i < heap; i++)
            {
                ItemBase tempItem = m_InventroyFactory.CreateItem(itemType, id, MAXITEMNUMBER);
                m_InventroyList.Add(tempItem);
            }
            if (heapRemainder > 0)
            {
                ItemBase tempItem = m_InventroyFactory.CreateItem(itemType, id, heapRemainder);
                m_InventroyList.Add(tempItem);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 当前最大仓库数量
        /// </summary>
        /// <returns></returns>
        public int CurMaxInventroyNumber()
        {
            if (m_InventroyList!=null)
            {
                return m_InventroyList.Count;
            }
            else
            {
                Log.e("InventroyList is null");
                return -1;
            }
        }
        /// <summary>
        /// 减少
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        public void DeleteItemForID(ItemID id,long number = 1)
        {
            List<ItemBase> itemBases = new List<ItemBase>();
            m_InventroyList.ForEach(i => {
                if (i.ID == id)
                    itemBases.Add(i);
            });
            if (itemBases.Count>0)
            {
                ItemBase itemBase = itemBases[0];
                itemBases.ForEach(i => {
                    if (i.Capacity< itemBase.Capacity)
                    {
                        itemBase = i;
                    }
                });
                
                if ((itemBase.Capacity - number)>0)
                {
                    itemBase.Capacity -= number;
                }
                else
                {
                    number -= itemBase.Capacity;
                    m_InventroyList.Remove(itemBase);
                    DeleteItemForID(id, number);
                }
            }
            else
            {
                Debug.Log("Not find Item ,ItemID = "+id);
            }
        }


        /// <summary>
        /// 往仓库中增加新物品(测试过一遍)
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="id"></param>
        /// <param name="number"></param>
        public void AddItemForID(ItemType itemType, ItemID id, long number = 1)
        {
            if (number == 0)
                return;

            ItemBase itemBase = GetItemForID(id);
            if (itemBase == null)//未找到新增
            {
                if (number <= MAXITEMNUMBER)//最大量不大于当前满量 
                {
                    ItemBase tempItem = m_InventroyFactory.CreateItem(itemType, id, number);
                    m_InventroyList.Add(tempItem);
                }
                else
                {
                    HandleSurplusItemNumber(itemType, id, number);
                }
            }
            else//找到了
            {
                long forecastQuantity = itemBase.Capacity + number;
                if (forecastQuantity< MAXITEMNUMBER)//未超 过满量
                {
                    itemBase.OnAddNumber(number);
                }
                else//满了
                {
                    long needNumber = MAXITEMNUMBER - itemBase.Capacity;
                    itemBase.OnAddNumber(needNumber);

                    long surplusNumber = number - needNumber;
                    HandleSurplusItemNumber(itemType, id, surplusNumber);
                }
            }
        }

        /// <summary>
        /// 获取所有的物品
        /// </summary>
        public List<ItemBase> GetAllItem()
        {
            return m_InventroyList;
        }

        /// <summary>
        /// 在列表中查找一个未满的物体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ItemBase GetItemForID(ItemID id)
        {
            ItemBase itemBase = null;
            m_InventroyList.ForEach(i =>
            {
                if (id == i.ID && !i.CapacityFull)
                {
                    itemBase = i;
                }
            });
            return itemBase;
        }
        /// <summary>
        /// 查找一个物品的总量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long GetItemTotalForID(ItemID id)
        {
            long total = 0;
            m_InventroyList.ForEach(i =>
            {
                if (id == i.ID)
                {
                    total += i.Capacity;
                }
            });
            return total;
        }


        #endregion
    }

}