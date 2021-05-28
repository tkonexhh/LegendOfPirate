using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Test : MonoBehaviour
	{
        private void Start()
        {
            CountdownActionNode countdownActionNode = ActionNode.Allocate<CountdownActionNode>();

            countdownActionNode.OnStartCallback += () => { Debug.Log("On countdown start"); };
            countdownActionNode.OnEndCallback += () => { Debug.Log("On countdown end"); };
            countdownActionNode.OnTickCallback += () => { Debug.Log("On countdown tick : " + Time.time); };

            countdownActionNode.Execute(this, System.DateTime.Now, 10, 1);
        }
    }
	
}