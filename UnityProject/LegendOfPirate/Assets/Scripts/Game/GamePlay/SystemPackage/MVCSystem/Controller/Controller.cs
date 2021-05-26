using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public abstract class Controller : IController, ICacheAble, ICacheType
    {
        #region IController
        public virtual void OnInit() { }
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

        //public static T Allocate<T>() where T : ICacheAble, new()
        //{
        //    return ObjectPool<T>.S.Allocate();
        //}


    }

}