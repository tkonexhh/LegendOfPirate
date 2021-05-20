using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public class DefaultConditionChecker : ConditionCheckerBase<int>
	{
        public override bool IsFinished()
        {
            return true;
        }
    }
	
}