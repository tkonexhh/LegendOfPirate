using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class RepeatActionNode : ActionNode
	{
        public RepeatActionNode() { }
        //private float m_DelayTime;

        //public RepeatActionNode(MonoBehaviour executeBehavior, DateTime startTime, float delayTime) : base(executeBehavior, startTime, delayTime)
        //{
        //    m_DelayTime = delayTime;
        //}

        //public override void Execute()
        //{
        //    base.Execute();

        //    if (DateTime.Now > m_EndTime)
        //    {
        //        OnEnd();
        //    }
        //    else
        //    {
        //        m_ExecuteBehavior.StartCoroutine(RepeatCor());
        //    }
        //}

        //private IEnumerator RepeatCor()
        //{
        //    yield return new WaitForSeconds(m_DelayTime);

        //    OnEnd();
        //}
    }
	
}