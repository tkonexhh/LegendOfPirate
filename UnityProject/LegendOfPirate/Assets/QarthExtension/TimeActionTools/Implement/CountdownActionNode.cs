using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class CountdownActionNode : TimeActionNode
	{
        public CountdownActionNode(System.DateTime startTime, float totalTime, float tickInterval) : base(startTime, totalTime)
        {

        }
	}
	
}