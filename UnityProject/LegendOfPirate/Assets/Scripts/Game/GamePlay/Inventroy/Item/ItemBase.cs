using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public abstract class ItemBase 
    {
        /// <summary>
        /// id
        /// </summary>
        public ItemID ID { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物品类型
        /// </summary>
        public ItemType Type { get; set; }
        /// <summary>
        /// 物品质量
        /// </summary>
        public ItemQuality Quality { get; set; }
        /// <summary>
        /// 物品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 物品容量
        /// </summary>
        public long Capacity { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public int BuyPrice { get; set; }
        /// <summary>
        /// 出售价格
        /// </summary>
        public int SellPrice { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Sprite { get; set; }
        /// <summary>
        /// 是否满容量
        /// </summary>
        public bool CapacityFull { get; set; }
        public ItemBase() 
        { }
        public ItemBase(ItemType itemType, ItemID id, long capacity) {
            this.ID = id;
            this.Type = itemType;
            this.Capacity = capacity;
            CheckCapacityIsFull();
            OnInitOtherInfo();
        }

        public abstract void OnInitOtherInfo();
        /// <summary>
        /// 增加数量
        /// </summary>
        /// <param name="number"></param>
        public void OnAddNumber(long number)
        {
            Capacity += number;
            CheckCapacityIsFull();
        }
        /// <summary>
        /// 检查容器是否满了
        /// </summary>
        private void CheckCapacityIsFull()
        {
            if (Capacity >= InventroyMgr.MAXITEMNUMBER)
            {
                CapacityFull = true;
            }
            else
            {
                CapacityFull = false;
            }
        }
    }

}