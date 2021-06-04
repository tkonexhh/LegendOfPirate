using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 重复执行某种操作
    /// </summary>
	public class RepeatNode : ActionNode
	{
        private float m_RepeatInterval;
        private float m_RepeatCount;

        private WaitForSeconds m_WaitForSeconds;

        private DateTime m_StartTime;

        public RepeatNode() { }

        public RepeatNode SetParams(MonoBehaviour executeBehavior, DateTime startTime, float repeatInterval, int repeatCount)
        {
            m_ExecuteBehavior = executeBehavior;

            m_RepeatInterval = repeatInterval;
            m_RepeatCount = repeatCount;

            m_StartTime = startTime;

            m_WaitForSeconds = new WaitForSeconds(repeatInterval);

            return this;
        }

        public override void Execute()
        {

            if (DateTime.Now > m_StartTime)
            {
                OnEnd();
            }
            else
            {
                m_ExecuteBehavior.StartCoroutine(RepeatCor());
            }
        }

        private IEnumerator RepeatCor()
        {
            OnTick();

            int curRepeatCount = 0;
            while (curRepeatCount < m_RepeatCount)
            {
                yield return m_WaitForSeconds;

                curRepeatCount++;

                OnTick();
            }

            OnEnd();
        }
    }
	
}