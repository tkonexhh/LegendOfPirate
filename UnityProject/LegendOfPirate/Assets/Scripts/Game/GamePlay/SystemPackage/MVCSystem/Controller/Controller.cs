using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public abstract class Controller : IController, ICacheAble, ICacheType
    {
        private bool m_FirstInit = false;

        #region IController
        public virtual void OnInit()
        {
            if (m_FirstInit == false)
            {
                m_FirstInit = true;
                OnFirstInit();
            }
        }
        public virtual void OnUpdate() { }
        public virtual void OnDestroyed() { }

        #endregion

        #region ICacheAble

        public bool cacheFlag
        {
            get; set;
        }

        public virtual void OnCacheReset()
        {

        }

        #endregion

        #region ICacheType

        public virtual void Recycle2Cache()
        {

        }

        #endregion

        #region  Override
        public virtual void OnFirstInit() { }
        #endregion

        //public static T Allocate<T>() where T : ICacheAble, new()
        //{
        //    return ObjectPool<T>.S.Allocate();
        //}


    }

}