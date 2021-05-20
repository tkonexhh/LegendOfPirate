using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ABTestNode : ExecuteNode
    {
        private ABTestModule m_ABTestModule;

        public override void OnBegin()
        {
            Log.i("ExecuteNode:" + GetType().Name);
            m_ABTestModule = GameMgr.S.AddCom<ABTestModule>();
        }

        public override void OnExecute()
        {
            if (m_ABTestModule != null)
                isFinish = m_ABTestModule.isABLoadFinish;
        }
    }
}
