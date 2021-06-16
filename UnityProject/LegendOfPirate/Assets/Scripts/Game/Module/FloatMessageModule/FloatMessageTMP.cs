//  Desc:        Framework For Game Develop with Unity3d
//  Copyright:   Copyright (C) 2017 SnowCold. All rights reserved.
//  WebSite:     https://github.com/SnowCold/Qarth
//  Blog:        http://blog.csdn.net/snowcoldgame
//  Author:      SnowCold
//  E-mail:      snowcold.ouyang@gmail.com
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class FloatMessageTMP : TSingleton<FloatMessageTMP>
    {
        public void ShowMsg(string msg)
        {
            FloatMessageTMPanel fP = UIMgr.S.FindPanel(UIID.FloatMessageTMPanel) as FloatMessageTMPanel;
            if (fP != null)
            {
                fP.ShowMsg(msg);
                return;
            }

            UIMgr.S.OpenPanel(UIID.FloatMessageTMPanel, (panel) =>
            {
                FloatMessageTMPanel panel1 = panel as FloatMessageTMPanel;
                panel1.ShowMsg(msg);
            },null);
        }

        public void ShowLightMsg(string msg)
        {
            FloatMessageTMPanel fP = UIMgr.S.FindPanel(EngineUI.LightMessagePanel) as FloatMessageTMPanel;
            if (fP != null)
            {
                fP.ShowMsg(msg);
                return;
            }

            UIMgr.S.OpenPanel(EngineUI.LightMessagePanel, (panel) =>
            {
                FloatMessageTMPanel panel1 = panel as FloatMessageTMPanel;
                panel1.ShowMsg(msg);
            });
        }
    }
}
