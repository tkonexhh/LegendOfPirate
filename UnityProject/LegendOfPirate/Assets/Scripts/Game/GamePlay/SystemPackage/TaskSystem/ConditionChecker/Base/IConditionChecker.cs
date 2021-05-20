using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public interface IConditionChecker
	{
        bool IsFinished();
        float GetProgressPercent();
        string GetTargetValue();
        string GetCurrentValue();
	}
	
}