using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
    public class TimeActionNode : ITimeActionNode
    {
        public Action OnStartCallback = null;
        public Action OnTickCallback = null;
        public Action OnEndCallback = null;

        protected DateTime m_StartTime;
        protected DateTime m_EndTime;
        protected float m_TotalTime;

        public TimeActionNode(DateTime startTime, float totalTime)
        {
            TimeSpan ts = TimeSpan.FromSeconds(totalTime);
            m_EndTime = startTime.Add(ts);
        }

        #region IActionNode

        public virtual void OnStart()
        {
            OnStartCallback?.Invoke();
        }

        public virtual void OnTick()
        {
            OnTickCallback?.Invoke();
        }

        public virtual void OnEnd()
        {
            OnEndCallback?.Invoke();
        }

        public virtual void Dispose()
        {
            OnStartCallback = null;
            OnTickCallback = null;
            OnEndCallback = null;
        }

        #endregion

        public void Execute()
        {
            OnStart();
        }
    }

}