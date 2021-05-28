using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
    public class TimeActionNode : ActionNode
    {
        protected DateTime m_StartTime;
        protected DateTime m_EndTime;
        protected float m_TotalTime;
        protected MonoBehaviour m_ExecuteBehavior;

        public TimeActionNode(MonoBehaviour executeBehavior, DateTime startTime, float totalTime)
        {
            m_ExecuteBehavior = executeBehavior;

            TimeSpan ts = TimeSpan.FromSeconds(totalTime);
            m_EndTime = startTime.Add(ts);
        }
    }

}