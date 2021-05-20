using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
namespace GameWish.Game
{
    public enum ScreenType
    {
        None,
        /// <summary>
        /// 一般机型
        /// </summary>
        Normal,
        /// <summary>
        /// 刘海机型  暂定根据屏幕比例来定
        /// </summary>
        IPhoneX,
    }

    public class ScreenAdjustMgr : TSingleton<ScreenAdjustMgr>
    {

        ScreenType m_ScreenType = ScreenType.None;
        float WedthHeightRatio = 0;
        float JudgeVaule = 2;
        public override void OnSingletonInit()
        {
            if (m_ScreenType == ScreenType.None)
                SetScreenType();
        }

        public void Init() { }

        public void SetScreenType()
        {
            float wedthHeightRatio = (float)Screen.width / (float)Screen.height;
            float heightWedthRatio = (float)Screen.height / (float)Screen.width;
            if (wedthHeightRatio >= JudgeVaule || heightWedthRatio >= JudgeVaule)
            {
                m_ScreenType = ScreenType.IPhoneX;
            }
            else
            {
                m_ScreenType = ScreenType.Normal;
            }
            WedthHeightRatio = wedthHeightRatio > 1 ? wedthHeightRatio : heightWedthRatio;
        }


        public ScreenType GetScreenType()
        {
            if (m_ScreenType == ScreenType.None)
                SetScreenType();
            return m_ScreenType;
        }

        public float GetWedthHeightRatio()
        {
            return WedthHeightRatio;
        }


    }
}
