using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
    public abstract class ActionNode : IActionNode
    {
        protected Action OnStartCallback = null;
        protected Action OnEndCallback = null;
        protected Action OnTickCallback = null;

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

        #endregion

        public ActionNode AddOnStartCallback(Action callback)
        {
            OnStartCallback += callback;

            return this;
        }

        public ActionNode AddOnTickCallback(Action callback)
        {
            OnTickCallback += callback;

            return this;
        }

        public ActionNode AddOnEndCallback(Action callback)
        {
            OnEndCallback += callback;

            return this;
        }

    }

}