using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using HedgehogTeam.EasyTouch;

namespace GameWish.Game
{
    public class CharaWordsCommand : AbstractGuideCommand
    {
        private bool m_NeedClose = true;//�ر��򵼽���
        private object[] m_Params;

        public override void SetParam(object[] pv)
        {
            if (pv.Length == 0)
            {
                Log.w("CharaWordsCommand Init With Invalid Param.");
                return;
            }

            try
            {
                m_NeedClose = Helper.String2Bool(pv[0].ToString());
                m_Params = pv;
            }
            catch (Exception e)
            {
                Log.e(e);
            }
        }

        protected override void OnStart()
        {
            UIMgr.S.OpenTopPanel(UIID.GuideWordsPanel, null, m_Params);

        }

        //���������Ҫִ�е�����
        protected override void OnFinish(bool forceClean)
        {
            //if (m_NeedClose || forceClean)
            {
                var panel = UIMgr.S.FindPanel(UIID.GuideWordsPanel);
                if (panel != null)
                {
                    (panel as AbstractPanel).CloseSelfPanel();
                }
            }
        }
    }
}
