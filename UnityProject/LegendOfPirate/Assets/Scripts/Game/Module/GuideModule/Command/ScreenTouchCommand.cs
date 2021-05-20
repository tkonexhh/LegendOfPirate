using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using HedgehogTeam.EasyTouch;

namespace GameWish.Game
{
    public class ScreenTouchCommand : AbstractGuideCommand
    {
        public override void SetParam(object[] pv)
        {
        }

        protected override void OnStart()
        {
            EasyTouch.On_TouchStart += OnTouchStart;
        }

        protected override void OnFinish(bool forceClean)
        {
            EasyTouch.On_TouchStart -= OnTouchStart;
        }

        void OnTouchStart(Gesture ges)
        {
            Log.w("=======screen touched======");
            FinishStep();
        }
    }
}
