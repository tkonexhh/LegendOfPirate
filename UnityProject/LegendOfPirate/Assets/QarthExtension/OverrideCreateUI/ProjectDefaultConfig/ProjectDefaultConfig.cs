/************************
	FileName:/GFrameWork/Scripts/Base/Config/ProjectDefaultConfig/ProjectDefaultConfig.cs
	CreateAuthor:neo.xu
	CreateTime:6/24/2020 2:25:48 PM
	Tip:6/24/2020 2:25:48 PM
************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth
{
    [System.Serializable]
    public class ProjectDefaultConfig : TScriptableObjectSingleton<ProjectDefaultConfig>
    {
        [Header("UGUI")]
        [SerializeField] private TextConfig m_TextConfig;
        [SerializeField] private TextMeshProConfig m_TMPConfig;


        public static TextConfig textConfig => S.m_TextConfig;
        public static TextMeshProConfig textMeshProConfig => S.m_TMPConfig;


    }

    [Serializable]
    public class TextConfig
    {
        [SerializeField] private Font m_DefaultTextFont;
        [SerializeField] private Color m_DefaultTextColor = Color.white;
        [SerializeField] private Vector2 m_DefaultTextRect = new Vector2(100, 50);
        [SerializeField] private int m_DefaultTextSize = 14;


        public Font defaultTextFont
        {
            get
            {
                if (m_DefaultTextFont == null)
                {
                    Resources.GetBuiltinResource<Font>("Arial.ttf");
                }
                return m_DefaultTextFont;
            }
            set
            {
                m_DefaultTextFont = value;
            }
        }

        public Color defaultTextColor => m_DefaultTextColor;
        public Vector2 defaultTextRect => m_DefaultTextRect;
        public int defaultTextSize => m_DefaultTextSize;
    }

}