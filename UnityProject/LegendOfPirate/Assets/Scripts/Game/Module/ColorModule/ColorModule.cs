using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class ColorModule : TSingleton<ColorModule>
    {
        private ColorConfig m_Config;

        public void Init()
        {
            Log.i("Init ColorModule.");
        }

        public ColorConfig colorConfig
        {
            get { return m_Config; }
        }

        public override void OnSingletonInit()
        {
            m_Config = LoadConfig();
        }

        public Color GetColor(float precent)
        {
            return ColorGradientHelper.CalcualteColor(m_Config.colors, precent);
        }

        private ColorConfig LoadConfig()
        {
            ResLoader loader = ResLoader.Allocate("ColorModule", null);

            UnityEngine.Object obj = loader.LoadSync("ColorConfig");
            if (obj == null)
            {
                Log.e("Not Find Color Config.");
                loader.Recycle2Cache();
                return null;
            }

            //Log.i("Success Load SDK Config.");
            ColorConfig prefab = obj as ColorConfig;

            ColorConfig newAB = GameObject.Instantiate(prefab);

            loader.Recycle2Cache();

            return newAB;
        }

        public Color GetColorByIndex(int index)
        {
            return m_Config.colors[index];
        }

    }
}
