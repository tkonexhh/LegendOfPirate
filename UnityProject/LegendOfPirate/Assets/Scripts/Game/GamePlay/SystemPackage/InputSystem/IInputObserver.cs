using HedgehogTeam.EasyTouch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public interface IInputObserver
    {
        //void On_MouseDown();
        //void On_MouseDrag(float deltaX, float deltaY, bool isStartFromUI);
        //void On_MouseUp();

        bool On_TouchStart(Gesture gesture);
        bool On_TouchDown(Gesture gesture);
        bool On_TouchUp(Gesture gesture);
        bool On_Swipe(Gesture gesture);
        bool On_Drag(Gesture gesture, bool isTouchStartFromUI);

        bool On_LongTap(Gesture gesture);

        bool BlockInput();

        int GetSortingLayer();
    }
}
