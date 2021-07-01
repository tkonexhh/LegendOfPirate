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
		public bool vipState;
		public DateTime vipPurchaseTime;

		public ConsumptionData() { }
	}
}