using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
    public class GuideModule : AbstractModule
    {
        public void StartGuide()
        {
            if (!AppConfig.S.isGuideActive)
            {
                return;
            }
            InitCustomTrigger();
            InitCustomCommand();
            GuideMgr.S.StartGuideTrack();
            
        }

        protected override void OnComAwake()
        {

        }
        protected void InitCustomTrigger()
        {
            //GuideMgr.S.RegisterGuideTrigger(typeof(GameStartTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(MyEventTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(FirstEnterBuildingTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(FirstOccupyBuildingTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(FirstEndFoodTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(BossMeetTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(RoadBlockTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(WeaponUnlockTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(MagicCloudTrigger));
            //GuideMgr.S.RegisterGuideTrigger(typeof(MagicCloudEndTriger));
            


        }

        protected void InitCustomCommand()
        {
            //GuideMgr.S.RegisterGuideCommand(typeof(MyDelayCommand));
            //GuideMgr.S.RegisterGuideCommand(typeof(GuideButtonCommand));
            //GuideMgr.S.RegisterGuideCommand(typeof(GuideTipsCommand));
            //GuideMgr.S.RegisterGuideCommand(typeof(NormalTipsCommand));
         
        }

    }
}
