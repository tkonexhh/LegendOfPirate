using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum FoodQualityType 
	{
		/// <summary>
		/// 白色-普通
		/// </summary>
		Normal,
		/// <summary>
		/// 绿色-进阶
		/// </summary>
		Advanced,
		/// <summary>
		/// 蓝色-稀有
		/// </summary>
		Rare,
		/// <summary>
		/// 紫色-史诗
		/// </summary>
		Epic,
		/// <summary>
		/// 红色-传说
		/// </summary>
		Legendary,
		/// <summary>
		/// 金色-不朽
		/// </summary>
		Immortal,
	}
}