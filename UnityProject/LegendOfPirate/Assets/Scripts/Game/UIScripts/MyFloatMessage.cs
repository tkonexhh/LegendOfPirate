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
using GameWish.Game;

namespace Qarth
{
    public class MyFloatMessage : TSingleton<MyFloatMessage>
    {
        public void ShowMsg(string msg)
        {
            MyFloatMessagePanel fP = UIMgr.S.FindPanel(UIID.MyFloatMessagePanel) as MyFloatMessagePanel;
            if (fP != null)
            {
                fP.ShowMsg(msg);
                return;
            }

            UIMgr.S.OpenTopPanel(UIID.MyFloatMessagePanel, (panel) =>
            {
                MyFloatMessagePanel panel1 = panel as MyFloatMessagePanel;
                panel1.ShowMsg(msg);
            });
        }
    }
}
