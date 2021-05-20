using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Qarth
{
    public class PlatformHelper
    {
        public static bool isAndroid
        {
            get
            {
                bool value = false;
#if UNITY_ANDROID
                value = true;
#endif
                return value;
            }
        }

        public static bool isEditor
        {
            get
            {
                bool value = false;
#if UNITY_EDITOR
                value = true;
#endif
                return value;
            }
        }

        public static bool isIOS
        {
            get
            {
                bool value = false;
#if UNITY_IOS
                value = true;
#endif
                return value;
            }
        }


        public static bool isMobile
        {
            get
            {
                bool value = false;

#if (UNITY_IOS || UNITY_ANDROID)
                value = true;
#endif
                return value;
            }
        }

        public static bool isAndroidSimulator
        {
            get
            {
                bool value = false;

#if UNITY_ANDROID
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject sensorManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "sensor");
                AndroidJavaObject sensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 5);
                if (sensor == null)
                {
                    value = true;
                }
#endif
                return value;
            }
        }


        private static bool IsLinuxSystem()
        {
            PlatformID platformID = System.Environment.OSVersion.Platform;

            if (platformID == PlatformID.MacOSX || platformID == PlatformID.Unix)
            {
                return true;
            }

            return false;
        }

        public static string GetPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                    return "macOs";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.WindowsEditor:
                    return "window";
                case RuntimePlatform.WindowsPlayer:
                    return "window";
                case RuntimePlatform.Android:
                    return "android";
                default:
                    return "null";
            }
        }
        //策划测试使用
        public static bool isPlanMode
        {
            get
            {
                //FIXME!!! TestMode True/False

                return false;
                return true;
            }
        }  

        public static bool isTestMode
        {
            get
            {
                //FIXME!!! TestMode True/False
                return false;
                return true;
                return isEditor;
            }
        }
    }

}