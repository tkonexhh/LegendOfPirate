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
		Weapon,
		/// <summary>
		/// 装备
		/// </summary>
		Armor,
		/// <summary>
		/// 饰品
		/// </summary>
		Jewelry,
		/// <summary>
		/// 专属神器
		/// </summary>
		Hallow,
	}
}