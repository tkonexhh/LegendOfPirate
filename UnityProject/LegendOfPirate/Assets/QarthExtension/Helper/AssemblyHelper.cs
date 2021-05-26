using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace GameWish.Game
{
	public class AssemblyHelper : MonoBehaviour
	{
        /// <summary>
        /// 获取 Assembly-CSharp 程序集
        /// </summary>
        public static Assembly DefaultCSharpAssembly
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(a => a.GetName().Name == "Assembly-CSharp");
            }
        }

        public static Assembly[] AllAssemblies
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }
        }
    }
	
}