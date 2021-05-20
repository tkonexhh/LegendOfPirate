using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using HedgehogTeam.EasyTouch;

namespace GameWish.Game
{
    public class ShowMonsterCommand : AbstractGuideCommand
    {
        public override void SetParam(object[] pv)
        {
        }

        protected override void OnStart()
        {
            //EasyTouch.On_TouchStart += OnTouchStart;
            //UnityExtensions.CallWithDelay(GameplayMgr.S.GetComponent<MonoBehaviour>(), () => {
            //    King king = GameObject.FindObjectOfType<King>();
            //    king.DoGuideShowAnim();
            //}, 1);

        }

        protected override void OnFinish(bool forceClean)
        {
            //EasyTouch.On_TouchStart -= OnTouchStart;
        }

        void OnTouchStart(Gesture ges)
        {
            //Log.w("=======screen touched======");
            //FinishStep();
        }
    }
}
