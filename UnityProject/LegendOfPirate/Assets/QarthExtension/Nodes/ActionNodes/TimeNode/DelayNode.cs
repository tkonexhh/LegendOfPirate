using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 延迟一段时间后处理某个操作
    /// </summary>
	public class DelayNode : ActionNode
	{
        private float m_DelayTime;
        private DateTime m_EndTime;

        public DelayNode SetParams(MonoBehaviour executeBehavior, DateTime startTime, float delayTime)
        {
            m_ExecuteBehavior = executeBehavior;

            TimeSpan ts = TimeSpan.FromSeconds(delayTime);
            m_EndTime = startTime.Add(ts);

            m_DelayTime = delayTime;

            return this;
        }

        public override void Execute()
        {
            if (DateTime.Now > m_EndTime)
            {
                OnEnd();
            }
            else
            {
                m_ExecuteBehavior.StartCoroutine(DelayCor());
            }
        }

        private IEnumerator DelayCor()
        {
            yield return new WaitForSeconds(m_DelayTime);

            OnEnd();
        }
    }
	
}