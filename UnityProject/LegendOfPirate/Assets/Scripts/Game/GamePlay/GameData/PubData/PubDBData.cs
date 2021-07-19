using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class PubDBData 
	{
		public DateTime lastRefreshTime;
		public int remainingTimes;

		public PubDBData()
		{
			lastRefreshTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
			remainingTimes = PubModel.DAILY_LIMIT;
		}
	}
}