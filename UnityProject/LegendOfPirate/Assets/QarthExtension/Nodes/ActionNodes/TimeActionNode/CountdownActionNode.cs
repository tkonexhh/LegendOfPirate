using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
	public class CountdownActionNode : TimeActionNode
	{
        private WaitForSeconds m_WaitForSeconds = null;

        public CountdownActionNode(MonoBehaviour executeBehavior, DateTime startTime, float totalTime, float tickInterval) : base(executeBehavior, startTime, totalTime)
        {
            m_WaitForSeconds = new WaitForSeconds(tickInterval);
        }

        public override void Execute()
        {
            base.Execute();

            if (DateTime.Now > m_EndTime)
            {
                OnEnd();
            }
            else
            {
                m_ExecuteBehavior.StartCoroutine(CountdownCor());
            }
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