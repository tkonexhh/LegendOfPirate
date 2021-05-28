using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Equipment : ItemBase
	{
        /// <summary>
        /// ����
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Intellect { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Agility { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int Stamina { get; set; }
        /// <summary>
        /// װ������
        /// </summary>
        public EquipmentType EquipType { get; set; }
        public Equipment(ItemType itemType, ItemID id, long capacity) : base(itemType, id, capacity)
        { }
        public override void OnInitOtherInfo()
        {
        }
    }
	
}