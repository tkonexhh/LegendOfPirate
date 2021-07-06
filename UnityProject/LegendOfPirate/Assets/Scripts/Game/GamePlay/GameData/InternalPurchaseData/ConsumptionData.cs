using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
	/// <summary>
	/// 消费数据
	/// </summary>
	[Serializable]
	public class ConsumptionData 
	{
		/// <summary>
		/// 是否是Vip
		/// </summary>
		public bool vipState;
		/// <summary>
		/// 自动续费状态
		/// </summary>
		public bool automaticRenewal;
		/// <summary>
		/// Vip购买时间
		/// </summary>
		public DateTime vipPurchaseTime;
		/// <summary>
		/// 今天是否领取
		/// </summary>
		public bool receiveToday;
		/// <summary>
		/// 上次领取时间
		/// </summary>
		public DateTime lastCollectionTime;
		/// <summary>
		/// 第一次领取的次数
		/// </summary>
		public int firstCollectionTimes;
		/// <summary>
		/// 领取的次数
		/// </summary>
		public int dailyCollectionTimes;
		/// <summary>
		/// 已经领取的钻石
		/// </summary>
		public int deceivedDiamondsNumber;

		public ConsumptionData() 
		{
			vipPurchaseTime = default(DateTime);
			lastCollectionTime = default(DateTime);
			vipState = false;
			automaticRenewal = false;
			receiveToday = false;
			dailyCollectionTimes = 0;
			firstCollectionTimes = 0;
			deceivedDiamondsNumber = 0;
		}
	}
}