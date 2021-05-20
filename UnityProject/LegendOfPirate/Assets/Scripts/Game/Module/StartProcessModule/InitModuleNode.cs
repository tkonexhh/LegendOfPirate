using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class InitModuleNode : ExecuteNode
    {
        public override void OnBegin()
        {
            Log.i("ExecuteNode:" + GetType().Name);
            AdsMgr.S.Init();
            AdsMgr.S.InitAllAdData();
            GameMgr.S.AddCom<UIDataModule>();
            GameMgr.S.AddCom<InputModule>();
            GameMgr.S.AddCom<CommonResModule>();
            GameMgr.S.AddCom<GuideModule>();
            //ColorModule.S.Init();
            //GameMgr.S.AddCom<GameplayMgr>();

            SignSystem.S.InitSignSystem();
            SecurityMgr.S.DoSecurityChecker();
            //PurchaseModule.S.Init();
            isFinish = true;
        }
    }
}
