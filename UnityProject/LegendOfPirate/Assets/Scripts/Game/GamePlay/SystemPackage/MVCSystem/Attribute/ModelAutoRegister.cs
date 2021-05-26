using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// Used this to auto register model to model mgr.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAutoRegisterAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ModelAutoRegisterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ModelAutoRegisterAttribute()
        {
        }
    }
	
}