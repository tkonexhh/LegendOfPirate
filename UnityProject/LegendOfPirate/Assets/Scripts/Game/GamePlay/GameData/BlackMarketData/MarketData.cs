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
	[Serializable]
	public class MarketData 
	{
		/// <summary>
		/// ˢ��ʱ�� 
		/// </summary>
		public DateTime refreshTime;
		/// <summary>
		/// ˢ�´���
		/// </summary>
		public int refreshCount;
		/// <summary>
		/// ��Ʒ�浵
		/// </summary>
		public List<CommodityDBData> commodityDBDatas;
		public MarketData()
		{
			refreshTime = default(DateTime);
			refreshCount = 0;
			commodityDBDatas = new List<CommodityDBData>();
		}
	}

	[Serializable]
	public class CommodityDBData
	{
		public int id;
		public int commodityID;
		public int surplus;

		public CommodityDBData() { }
		public CommodityDBData(int id,int commodityID, int surplus) 
		{
			this.id = id;
			this.commodityID = commodityID;
			this.surplus = surplus;
		}
	}
}