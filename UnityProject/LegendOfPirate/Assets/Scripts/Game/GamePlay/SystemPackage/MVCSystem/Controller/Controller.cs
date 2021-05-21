using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public abstract class Controller : IController, ICacheAble
    {
        #region ICacheAble

        public bool cacheFlag { get; set; }

        public virtual void OnCacheReset()
        {

        }

        #endregion

        public static T Allocate<T>() where T : ICacheAble, new()
        {
            return ObjectPool<T>.S.Allocate();
        }
    }

}