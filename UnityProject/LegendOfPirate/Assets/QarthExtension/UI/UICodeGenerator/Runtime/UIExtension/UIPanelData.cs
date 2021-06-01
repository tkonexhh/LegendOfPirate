using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace Qarth.Extension
{
    /// <summary>
    /// 每个UIbehaviour对应的Data
    /// </summary>
    public interface IUIData : ICacheAble
    {
    }

    public class UIPanelData : IUIData
    {
        protected AbstractPanel mPanel;

        public bool cacheFlag { get; set; }

        public static T Allocate<T>() where T:IUIData, new()
        {
            return ObjectPool<T>.S.Allocate();
        }

        public static void Recycle2Cache<T>(T data) where T : IUIData, new()
        {
            ObjectPool<T>.S.Recycle(data);
        }

        public virtual void OnCacheReset()
        {

        }
    }

}