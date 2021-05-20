using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using HedgehogTeam.EasyTouch;
using UnityEngine.EventSystems;

namespace GameWish.Game
{
    public class UIClipCommand : AbstractGuideCommand
    {
        private IUINodeFinder m_Finder;
        protected Transform m_TargetButton;
        private float m_Raduis;
        private Vector3 m_Offset;
        private bool m_FinishStep = true;

        private bool m_HasDown = false;
        private static List<RaycastResult> m_Result = new List<RaycastResult>();

        #region SET
        protected IUINodeFinder GetNodeFinder
        {
            get
            {
                return m_Finder;
            }
        }
        protected bool IsFinishStep
        {
            get
            {
                return m_FinishStep;
            }
        }
        protected Vector3 Offset
        {
            get
            {
                return m_Offset;
            }
        }
        protected float GetRaduis
        {
            get
            {
                return m_Raduis;
            }
        }
        protected bool HasDown
        {
            get
            {
                return m_HasDown;
            }
        }
        #endregion

        public override void SetParam(object[] param)
        {
            if (param == null || param.Length != 4)
            {
                Log.w("UIClipCommand param error");
            }

            if (param.Length > 0)
            {
                m_Finder = param[0] as IUINodeFinder;
            }
            if (param.Length > 1)
            {
                m_Raduis = Helper.String2Float((string)param[1]);
            }
            if (param.Length > 2)
            {
                m_Offset = Helper.String2Vector3((string)param[2], '|');
            }
            if (param.Length > 3)
            {
                m_FinishStep = Helper.String2Bool((string)param[3]);
            }
        }

        protected override void OnStart()
        {
            m_TargetButton = m_Finder.FindNode(false);

            if (m_TargetButton == null)
            {
                //Log.e("Can't find targe ui node");
                return;
            }

            AppLoopMgr.S.onUpdate += Update;

            UIMgr.S.topPanelHideMask = PanelHideMask.UnInteractive;

            UIMgr.S.OpenTopPanel(UIID.UIClipPanel, null, m_TargetButton, m_Raduis, m_Offset);

        }

        protected override void OnFinish(bool forceClean)
        {
            UIMgr.S.topPanelHideMask = PanelHideMask.None;

            AppLoopMgr.S.onUpdate -= Update;

            UIMgr.S.ClosePanelAsUIID(UIID.UIClipPanel);
        }

        protected virtual void OnClickUpOnTarget()
        {
            ExecuteEvents.Execute<IPointerClickHandler>(m_TargetButton.gameObject, new PointerEventData(UnityEngine.EventSystems.EventSystem.current), ExecuteEvents.pointerClickHandler);
           
            if(m_FinishStep)
                FinishStep();

        }

        private void OnClickDownOnTarget()
        {
            ExecuteEvents.Execute<IPointerDownHandler>(m_TargetButton.gameObject, new PointerEventData(UnityEngine.EventSystems.EventSystem.current), ExecuteEvents.pointerDownHandler);
        }

        protected void Update()
        {
            if (m_TargetButton == null)
            {
                AppLoopMgr.S.onUpdate -= Update;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (CheckIsTouchInTarget())
                {
                    m_HasDown = true;
                    OnClickDownOnTarget();
                }
            }
            if (!m_HasDown)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (CheckIsTouchInTarget())
                {
                    OnClickUpOnTarget();
                }
                m_HasDown = false;
            }
        }

        protected virtual bool CheckIsTouchInTarget()
        {
            PointerEventData pd = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            pd.position = Input.mousePosition;

            var graphicRaycasr = m_TargetButton.GetComponentInParent<GraphicRaycaster>();

            if (graphicRaycasr == null)
            {
                return false;
            }

            graphicRaycasr.Raycast(pd, m_Result);

            if (m_Result.Count == 0)
            {
                return false;
            }

            if (IsHitWhiteObject(m_Result))
            {
                m_Result.Clear();
                return true;
            }

            m_Result.Clear();
            return false;
        }

        private bool IsHitWhiteObject(List<RaycastResult> result)
        {
            if (result == null || result.Count == 0)
            {
                return false;
            }

            for (int i = result.Count - 1; i >= 0; --i)
            {
                GameObject go = result[i].gameObject;
                if (go != null)
                {
                    if (IsHitWhiteObject(go.transform))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected virtual bool IsHitWhiteObject(Transform tr)
        {
            if (tr.IsChildOf(m_TargetButton.transform))
            {
                return true;
            }

            return false;
        }
   
    }
}
