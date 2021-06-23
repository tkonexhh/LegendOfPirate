using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum  MaterialOutputWay 
	{
		/// <summary>
		/// 资源岛
		/// </summary>
		ResourceIsland = 1,
		/// <summary>
		/// 战斗奖励
		/// </summary>
		BattleReward = 2,
		/// <summary>
		/// 菜园
		/// </summary>
		VegetableGarden = 3,
		/// <summary>
		/// 钓鱼台
		/// </summary>
		DiaoYuTai = 4,
		/// <summary>
		/// 加工室
		/// </summary>
		ProcessingRoom = 5,
		/// <summary>
		/// 商城
		/// </summary>
		ShoppingMall = 6,
		/// <summary>
		/// 活动
		/// </summary>
		Activity = 7,
	}
}