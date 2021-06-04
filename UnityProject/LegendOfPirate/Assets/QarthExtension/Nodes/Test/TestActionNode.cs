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
            //TestSequenceNode();
            TestParallelNode();
        }

        private void TestScheduleNode()
        {
            ScheduleNode countdownActionNode = ActionNode.Allocate<ScheduleNode>();

            countdownActionNode.SetParams(this, System.DateTime.Now, 10, 1)
                .AddOnStartCallback((node) => { Debug.Log("On countdown start"); })
                .AddOnTickCallback((node) => { Debug.Log("On countdown tick : " + Time.time); })
                .AddOnEndCallback((node) => { Debug.Log("On countdown end"); ActionNode.Recycle2Cache(countdownActionNode); })
                .Execute();
        }

        private void TestSequenceNode()
        {
            DelayNode delayNode1 = ActionNode.Allocate<DelayNode>();
            delayNode1.SetParams(this, DateTime.Now, 3)
                .AddOnStartCallback((node) => {
                    Debug.Log("Delay1 start");
                })
                .AddOnEndCallback((node)=> {
                    Debug.Log("Delay1 end"); });

            DelayNode delayNode2 = ActionNode.Allocate<DelayNode>();
            delayNode2.SetParams(this, DateTime.Now, 6)
                .AddOnStartCallback((node) => {
                    Debug.Log("Delay2 start");
                })
                .AddOnEndCallback((node) => {
                    Debug.Log("Delay2 end"); });

            SequenceNode sequenceNode = ActionNode.Allocate<SequenceNode>();
            sequenceNode.SetParams(this).Append(delayNode1).Append(delayNode2)
                .AddOnEndCallback((node) => { Debug.Log("SequenceNode end"); })
                .Execute();

        }

        private void TestParallelNode()
        {
            DelayNode delayNode1 = ActionNode.Allocate<DelayNode>();
            delayNode1.SetParams(this, DateTime.Now, 3)
                .AddOnStartCallback((node) => {
                    Debug.Log("Delay1 start");
                })
                .AddOnEndCallback((node) => {
                    Debug.Log("Delay1 end");
                });

            DelayNode delayNode2 = ActionNode.Allocate<DelayNode>();
            delayNode2.SetParams(this, DateTime.Now, 6)
                .AddOnStartCallback((node) => {
                    Debug.Log("Delay2 start");
                })
                .AddOnEndCallback((node) => {
                    Debug.Log("Delay2 end");
                });

            ParallelNode parallelNode = ActionNode.Allocate<ParallelNode>();
            parallelNode.SetParams(this).Add(delayNode1).Add(delayNode2)
                .AddOnEndCallback((node) => { Debug.Log("ParallelNode end"); })
                .Execute();

        }
    }
	
}