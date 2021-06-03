using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
	public class TestActionNode : MonoBehaviour
	{
        private void Start()
        {
            //TestScheduleNode();
            TestSequenceNode();
        }

        private void TestScheduleNode()
        {
            ScheduleNode countdownActionNode = ActionNode.Allocate<ScheduleNode>();

            countdownActionNode.SetParams(this, System.DateTime.Now, 10, 1)
                .AddOnStartCallback(() => { Debug.Log("On countdown start"); })
                .AddOnTickCallback(() => { Debug.Log("On countdown tick : " + Time.time); })
                .AddOnEndCallback(() => { Debug.Log("On countdown end"); ActionNode.Recycle2Cache(countdownActionNode); })
                .Execute();
        }

        private void TestSequenceNode()
        {
            DelayNode delayNode1 = ActionNode.Allocate<DelayNode>();
            delayNode1.SetParams(this, DateTime.Now, 3)
                .AddOnEndCallback(()=> {
                    Debug.Log("Delay1 end"); });

            DelayNode delayNode2 = ActionNode.Allocate<DelayNode>();
            delayNode2.SetParams(this, DateTime.Now, 6)
                .AddOnEndCallback(() => {
                    Debug.Log("Delay2 end"); });

            SequenceNode sequenceNode = ActionNode.Allocate<SequenceNode>();
            sequenceNode.SetParams(this).Append(delayNode1).Append(delayNode2)
                .AddOnEndCallback(() => { Debug.Log("SequenceNode end"); })
                .Execute();

        }
    }
	
}