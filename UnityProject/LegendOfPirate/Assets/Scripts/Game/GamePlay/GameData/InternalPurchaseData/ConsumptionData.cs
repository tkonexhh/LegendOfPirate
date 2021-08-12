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
	/// ��������
	/// </summary>
	[Serializable]
	public class ConsumptionData
	{
		#region Vip
		/// <summary>
		/// �Ƿ���Vip
		/// </summary>
		public bool vipState;
		/// <summary>
		/// �Զ�����״̬
		/// </summary>
		public bool automaticRenewal;
		/// <summary>
		/// Vip����ʱ��
		/// </summary>
		public DateTime vipPurchaseTime;
		/// <summary>
		/// �����Ƿ���ȡ
		/// </summary>
		public bool receiveToday;
		/// <summary>
		/// �ϴ���ȡʱ��
		/// </summary>
		public DateTime lastCollectionTime;
		/// <summary>
		/// ��һ����ȡ�Ĵ���
		/// </summary>
		public int firstCollectionTimes;
		/// <summary>
		/// ��ȡ�Ĵ���
		/// </summary>
		public int dailyCollectionTimes;
		/// <summary>
		/// �Ѿ���ȡ����ʯ
		/// </summary>
		public int deceivedDiamondsNumber;
		#endregion

		#region DailySelection
		/// <summary>
		/// �ճ�������ʼʱ��
		/// </summary>
		public DateTime dailyInitialTime;
		/// <summary>
		/// ��¼���ճ���������
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