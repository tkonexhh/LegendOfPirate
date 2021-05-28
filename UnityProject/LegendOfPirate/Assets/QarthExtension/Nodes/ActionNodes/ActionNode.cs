using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
    public abstract class ActionNode : IActionNode
    {
        public Action OnStartCallback = null;
        public Action OnEndCallback = null;
        public Action OnTickCallback = null;

        protected bool m_IsFinished = false;

        public bool cacheFlag { get; set; }

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

        }
        #endregion

        //public virtual void Execute()
        //{
        //    OnStart();
        //}

    }

}