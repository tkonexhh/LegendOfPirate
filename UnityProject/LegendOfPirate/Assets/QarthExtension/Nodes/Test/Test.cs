using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public class Test : MonoBehaviour
	{
        private void Start()
        {
            ScheduleNode countdownActionNode = ActionNode.Allocate<ScheduleNode>();

            countdownActionNode.SetParams(this, System.DateTime.Now, 10, 1)
                .AddOnStartCallback(() => { Debug.Log("On countdown start"); })
                .AddOnTickCallback(() => { Debug.Log("On countdown tick : " + Time.time);  })
                .AddOnEndCallback(() => { Debug.Log("On countdown end"); ActionNode.Recycle2Cache(countdownActionNode); })
                .Execute();
        }
    }
	
}