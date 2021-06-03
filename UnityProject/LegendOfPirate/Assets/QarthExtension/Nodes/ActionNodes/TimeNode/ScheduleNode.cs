using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
	public class ScheduleNode : ActionNode
	{
        private DateTime m_EndTime;
        private WaitForSeconds m_WaitForSeconds = null;

        public ScheduleNode() { }

        public ScheduleNode SetParams(MonoBehaviour executeBehavior, DateTime startTime, float totalTime, float tickInterval)
        {
            m_ExecuteBehavior = executeBehavior;

            TimeSpan ts = TimeSpan.FromSeconds(totalTime);
            m_EndTime = startTime.Add(ts);

            m_WaitForSeconds = new WaitForSeconds(tickInterval);

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
                m_ExecuteBehavior.StartCoroutine(CountdownCor());
            }
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();

            Recycle2Cache(this);
        }

        public override void OnCacheReset()
        {
            base.OnCacheReset();

            m_WaitForSeconds = null;
        }

        private IEnumerator CountdownCor()
        {
            OnTick();

            while (m_EndTime > DateTime.Now)
            {                    
                yield return m_WaitForSeconds;

                OnTick();
            }

            OnEnd();
        }
    }
	
}