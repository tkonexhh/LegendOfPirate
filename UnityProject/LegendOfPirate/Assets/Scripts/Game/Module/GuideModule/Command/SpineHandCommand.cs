
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;

namespace Qarth
{
    public class SpineHandCommand : AbstractGuideCommand
    {
        private IUINodeFinder m_Finder;
        private bool m_NeedClose = true;
        private Vector3 m_Offset;
        private Vector3 m_MoveToPos;
        private string m_SpineAnimName;
        private bool m_ShowGuideBtn;

        public override void SetParam(object[] pv)
        {
            if (pv.Length == 0)
            {
                Log.w("GuideHandCommand Init With Invalid Param.");
                return;
            }

            try
            {
                m_Finder = pv[0] as IUINodeFinder;

                if (pv.Length > 1)
                {
                    m_NeedClose = Helper.String2Bool((string)pv[1]);
                }
                if (pv.Length > 2)
                {
                    m_Offset = Helper.String2Vector3((string) pv[2], '|');
                }
                if (pv.Length > 3)
                {
                    m_MoveToPos = Helper.String2Vector3((string)pv[3], '|');
                }
                if (pv.Length > 4)
                {
                    m_SpineAnimName = (string)pv[4];
                }
                if (pv.Length > 5)
                {
                    m_ShowGuideBtn = Helper.String2Bool((string)pv[5]);
                }

            }
            catch (Exception e)
            {
                Log.e(e);
            }
        }

        protected override void OnStart()
        {
            RectTransform targetNode = m_Finder.FindNode(false) as RectTransform;

            if (targetNode == null)
            {
                Log.w("======transform is null=======");
                return;
            }
           // UIMgr.S.OpenTopPanel(UIID.GuideSpineHandPanel, null, targetNode, m_Offset, m_MoveToPos,m_SpineAnimName);
        }

        protected override void OnFinish(bool forceClean)
        {
            if (m_NeedClose || forceClean)
            {
              //  UIMgr.S.ClosePanelAsUIID(UIID.GuideSpineHandPanel);
            }
        }
    }
}

