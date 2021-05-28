using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class DelayActionNode : TimeActionNode
	{
        private float m_DelayTime;

        public DelayActionNode(MonoBehaviour executeBehavior, DateTime startTime, float delayTime) : base(executeBehavior, startTime, delayTime)
        {
            m_DelayTime = delayTime;
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