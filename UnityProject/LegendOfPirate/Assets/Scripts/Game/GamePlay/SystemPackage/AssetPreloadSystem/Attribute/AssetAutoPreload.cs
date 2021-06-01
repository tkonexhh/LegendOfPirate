using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// Used this to auto register AssetPreloader to AssetPreloaderMgr.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AssetAutoPreloadAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public AssetAutoPreloadAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AssetAutoPreloadAttribute()
        {
        }
    }
	
}