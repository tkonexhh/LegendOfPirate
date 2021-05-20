using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class StartProcessModule : AbstractStartProcess
    {

        protected override void InitExechuteContainer()
        {
            Append(new InitEnvironmentNode());
            //开启加载界面
            Append(new LoadTableNode());
            Append(new InitModuleNode());
            Append(new LoginNode());
            //关闭加载界面

            Append(new ABTestNode());
        }
    }
}
