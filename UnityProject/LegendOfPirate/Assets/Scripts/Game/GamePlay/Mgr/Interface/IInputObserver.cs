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

        void On_TouchStart(Gesture gesture);
        void On_TouchDown(Gesture gesture);
        void On_TouchUp(Gesture gesture);
        void On_Swipe(Gesture gesture);
        void On_Drag(Gesture gesture, bool isTouchStartFromUI);

        void On_LongTap(Gesture gesture);
    }
}
