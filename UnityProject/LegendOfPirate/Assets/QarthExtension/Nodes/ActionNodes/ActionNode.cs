using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
    public class ActionNode : IActionNode
    {
        public Action OnStartCallback = null;
        public Action OnEndCallback = null;
        public Action OnTickCallback = null;

        protected bool m_IsFinished = false;

        public ActionNode()
        {

        }

        #region IActionNode

        public virtual void OnStart()
        {
            m_IsFinished = false;

            OnStartCallback?.Invoke();
        }

        public virtual void OnTick()
        {
            OnTickCallback?.Invoke();
        }

        public virtual void OnEnd()
        {
            m_IsFinished = true;

            OnEndCallback?.Invoke();
        }

        public virtual void Dispose()
        {
            OnStartCallback = null;
            OnTickCallback = null;
            OnEndCallback = null;
        }

        #endregion

        public virtual void Execute()
        {
            OnStart();
        }
    }

}