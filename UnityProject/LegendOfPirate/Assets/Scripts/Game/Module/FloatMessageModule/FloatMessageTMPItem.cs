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
using UnityEngine.UI;
using TMPro;

namespace GameWish.Game
{
    public class FloatMessageTMPItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        public void SetFloatMsg(FloatTMPMsg msg)
        {
            if (msg == null)
            {
                return;
            }

            m_Text.text = msg.message;
        }
    }
}
