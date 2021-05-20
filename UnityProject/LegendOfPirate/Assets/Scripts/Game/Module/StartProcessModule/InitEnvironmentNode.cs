using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class InitEnvironmentNode : ExecuteNode
    {
        public override void OnBegin()
        {
            Log.i("ExecuteNode:" + GetType().Name);
            ResMgr.S.InitResMgr();
            AppConfig.S.InitAppConfig();
            RandomHelper.seed = (int)DateTime.Now.Ticks;

            if (AppConfig.S.dumpToFile)
            {
                //DebugLogger.S.InitDebugLogger();
            }

            isFinish = true;
        }
    }
}
