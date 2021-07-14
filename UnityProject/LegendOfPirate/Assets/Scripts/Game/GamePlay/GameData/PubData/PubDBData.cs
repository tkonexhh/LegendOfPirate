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
			lastRefreshTime = default(DateTime);
			remainingTimes = PubModel.DAILY_LIMIT;
		}
	}
}