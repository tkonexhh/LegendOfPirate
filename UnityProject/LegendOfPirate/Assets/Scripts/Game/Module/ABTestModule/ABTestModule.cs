using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ABTestModule : AbstractModule
    {
        private bool m_IsABLoadFinish = false;
        private int m_WaitLoadMaxSec = 3;
        private int m_TimerId = -1;

        public bool isABLoadFinish
        {
            get { return m_IsABLoadFinish; }
        }

        protected override void OnComAwake()
        {
            //module开始时做的事情
            m_IsABLoadFinish = false;
            m_TimerId = -1;
            //actor.StartCoroutine(ABTestMgr.S.CheckABRemoteInit(OnABLoadFinish));

            actor.StartCoroutine(ABTestMgr_CHS.S.CheckABRemoteInit(OnABLoadFinish));
        }

        private void OnABLoadFinish(bool success)
        {
            m_IsABLoadFinish = true;

            Log.i("AB test module load finish : " + success);

            //if (success == false)
            //{
            //    DataAnalysisMgr.S.CustomEventWithDate("AB_test_module_load_failed");
            //}

            //EventSystem.S.Send(EventID.OnUpdateLoadProgress, 0.2f);
        }

        private bool IsFirstInstall()
        {
            bool isFirstInstall = PlayerPrefs.GetInt("IsFirstInstall", 1) == 1;
            return isFirstInstall;
        }
    }
}
