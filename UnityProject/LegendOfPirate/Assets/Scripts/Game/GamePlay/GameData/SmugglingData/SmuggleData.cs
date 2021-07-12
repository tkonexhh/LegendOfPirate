using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
	public class SmuggleData : IDataClass
	{
		public List<SmuggleOrderData> orderDataList = new List<SmuggleOrderData>();

        public override void InitWithEmptyData()
        {
			for (int i = 1; i <= TDSmuggleTable.dataList.Count; i++) 
			{
				orderDataList.Add(new SmuggleOrderData(i));
			}
        }

        public override void OnDataLoadFinish()
        {
            
        }
    }

	[SerializeField]
	public struct SmuggleOrderData 
	{
		public int orderId;
		public int heroId;
		public DateTime smuggingStartTime;
		public OrderState orderState;

		private SmuggleData m_SmuggleData;

		public SmuggleOrderData(int orderid) 
		{
			m_SmuggleData = null;
			this.orderId = orderid;
			smuggingStartTime = default(DateTime);
			orderState = OrderState.Free;
			heroId = -1;
		}

		public void OnStartOrder(int heroId, DateTime time)
		{
			this.heroId = heroId;
			this.smuggingStartTime = time;
			orderState = OrderState.Smugging;

			SetDataDirty();
		}

		public void OnSmugglingFinish() 
		{
			heroId = -1;
			this.smuggingStartTime = default(DateTime);
			orderState = OrderState.Complate;

			SetDataDirty();
		}

		public void OnEndOrder() 
		{
			orderState = OrderState.Free;
			SetDataDirty();
		}

		public void OnUnlocked() 
		{
			orderState = OrderState.Free;

			SetDataDirty();
		}

		private void SetDataDirty() 
		{
			if (m_SmuggleData == null) 
			{
				m_SmuggleData = GameDataMgr.S.GetData<SmuggleData>();
			}
			m_SmuggleData.SetDataDirty();
		}
	}
}