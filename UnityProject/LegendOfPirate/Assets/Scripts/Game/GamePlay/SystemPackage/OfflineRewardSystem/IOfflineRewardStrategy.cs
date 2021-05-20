using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public interface IOfflineRewardStrategy
    {
        double GetReward(double offlineTime);
	}
	
}