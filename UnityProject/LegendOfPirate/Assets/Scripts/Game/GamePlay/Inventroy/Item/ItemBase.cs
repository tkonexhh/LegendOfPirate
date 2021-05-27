using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ItemBase 
	{
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
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
        public int Capacity { get; set; }
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
    }
	
}