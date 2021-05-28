using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Equipment : ItemBase
	{
        /// <summary>
        /// 力量
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// 智力
        /// </summary>
        public int Intellect { get; set; }
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agility { get; set; }
        /// <summary>
        /// 体力
        /// </summary>
        public int Stamina { get; set; }
        /// <summary>
        /// 装备类型
        /// </summary>
        public EquipmentType EquipType { get; set; }
        public Equipment(ItemType itemType, ItemID id, long capacity) : base(itemType, id, capacity)
        { }
        public override void OnInitOtherInfo()
        {
        }
    }
	
}