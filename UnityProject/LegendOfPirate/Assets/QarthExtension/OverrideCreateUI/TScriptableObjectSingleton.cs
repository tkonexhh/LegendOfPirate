using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    public class TScriptableObjectSingleton<T> : ScriptableObject where T : TScriptableObjectSingleton<T>
    {

        private static string PROJECT_CONFIG_PATH = "Resources/Config/" + typeof(T).Name;
        private static T s_Instance = null;


        private static T LoadInstance()
        {
            UnityEngine.Object obj = Resources.Load(ResourcesPath2Path(PROJECT_CONFIG_PATH));

            if (obj == null)
            {
                Debug.LogError("#Not Find " + typeof(T).Name + " Config File");
                return null;
            }

            // Debug.Log("#Success Load " + typeof(T).Name + " Config.");

            s_Instance = obj as T;

            return s_Instance;
        }


        public static T S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = LoadInstance();
                }

                return s_Instance;
            }
        }

        public static string ResourcesPath2Path(string path)
        {
            return path.Substring(10);
        }
    }

}