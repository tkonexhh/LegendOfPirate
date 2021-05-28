using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Weapon : ItemBase
	{
		/// <summary>
		/// 伤害
		/// </summary>
		public int Damage { get; set; }
		/// <summary>
		/// 加成
		/// </summary>
		public int Addition { get; set; }
		/// <summary>
		/// 穿戴方式
		/// </summary>
		public WeaponType WeaponType { get; set; }

		public Weapon(ItemType itemType, ItemID id, long capacity) : base(itemType, id, capacity)
		{ }
		public override void OnInitOtherInfo()
        {
        }
    }
	
}