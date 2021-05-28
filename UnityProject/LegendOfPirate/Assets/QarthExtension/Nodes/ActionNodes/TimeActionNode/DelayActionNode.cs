using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class DelayActionNode : ActionNode
	{
        private float m_DelayTime;

        public void Execute(MonoBehaviour executeBehavior, DateTime startTime, float delayTime)
        {
            TimeSpan ts = TimeSpan.FromSeconds(delayTime);
            DateTime endTime = startTime.Add(ts);

            m_DelayTime = delayTime;

            if (DateTime.Now > endTime)
            {
                OnEnd();
            }
            else
            {
                executeBehavior.StartCoroutine(DelayCor());
            }
        }

        //public override void Execute()
        //{
        //    base.Execute();

        //    if (DateTime.Now > m_EndTime)
        //    {
        //        OnEnd();
        //    }
        //    else
        //    {
        //        m_ExecuteBehavior.StartCoroutine(DelayCor());
        //    }
        //}

        private IEnumerator DelayCor()
        {
            yield return new WaitForSeconds(m_DelayTime);

            OnEnd();
        }
    }
	
}