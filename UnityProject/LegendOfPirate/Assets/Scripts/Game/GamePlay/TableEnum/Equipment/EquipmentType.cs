using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum EquipmentType
	{
		/// <summary>
		/// 武器
		/// </summary>
		Weapon=0,
		/// <summary>
		/// 装备
		/// </summary>
		Armor=1,
		/// <summary>
		/// 饰品
		/// </summary>
		Jewelry=2,
		/// <summary>
		/// 专属神器
		/// </summary>
		Hallow=3,
	}
}