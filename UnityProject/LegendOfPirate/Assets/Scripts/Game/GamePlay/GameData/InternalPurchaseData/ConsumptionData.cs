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
		#region Vip
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
		#endregion

		#region DailySelection
		/// <summary>
		/// 日常奖励初始时间
		/// </summary>
		public DateTime dailyInitialTime;
		/// <summary>
		/// 记录的日常奖励数据
		/// </summary>
		public List<DailyDBData> dailyDataModels = new List<DailyDBData>();
		#endregion

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

			dailyInitialTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
		}
	}

	public class DailyDBData
	{
		public int id;
		public PurchaseState purchaseState;
		public DailSelectionType dailSelectionType;
		public DailyDBData()
		{ }
		public DailyDBData(int id, DailSelectionType dailSelectionType)
		{
			this.id = id;
			this.dailSelectionType = dailSelectionType;
			this.purchaseState = PurchaseState.NotPurchased;
		}
	}
}