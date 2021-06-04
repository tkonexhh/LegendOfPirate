using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
    public abstract class ActionNode : IActionNode
    {
        protected Action<ActionNode> OnStartCallback = null;
        protected Action<ActionNode> OnEndCallback = null;
        protected Action<ActionNode> OnTickCallback = null;

        protected MonoBehaviour m_ExecuteBehavior = null;

        protected bool m_IsFinished = false;

        public bool cacheFlag { get; set; }

        public bool IsFinished => m_IsFinished;

        public ActionNode()
        {

        }

        public static T Allocate<T>() where T : ICacheAble, new ()
        {
            return ObjectPool<T>.S.Allocate();
        }

        public static void Recycle2Cache<T>(T node) where T : ICacheAble, new()
        {
            ObjectPool<T>.S.Recycle(node);
        }

        #region IActionNode

        public virtual void OnStart()
        {
            m_IsFinished = false;

            OnStartCallback?.Invoke(this);
        }

        public virtual void OnTick()
        {
            OnTickCallback?.Invoke(this);
        }

        public virtual void OnEnd()
        {
            m_IsFinished = true;

            OnEndCallback?.Invoke(this);
        }

        public virtual void Dispose()
        {
            OnStartCallback = null;
            OnTickCallback = null;
            OnEndCallback = null;
        }

        public virtual void Recycle2Cache()
        {

        }

        public virtual void OnCacheReset()
        {
            OnStartCallback = null;
            OnEndCallback = null;
            OnTickCallback = null;
        }

        public virtual void Execute()
        {

        }
        public ActionNode AddOnStartCallback(Action<ActionNode> callback)
        {
            OnStartCallback += callback;

            return this;
        }

        public ActionNode AddOnTickCallback(Action<ActionNode> callback)
        {
            OnTickCallback += callback;

            return this;
        }

        public ActionNode AddOnEndCallback(Action<ActionNode> callback)
        {
            OnEndCallback += callback;

            return this;
        }
        #endregion



    }

}