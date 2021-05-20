﻿using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkipGuideCommand : AbstractGuideCommand
{


    private IUINodeFinder m_Finder;
    private Transform m_TargetButton;
    private Transform m_BgBtn;
    private bool m_HasDown = false;
    private bool m_FinishGuide;
    private static List<RaycastResult> m_Result = new List<RaycastResult>();
    private bool m_NeedBg;

    public override void SetParam(object[] param)
    {
        if (param.Length > 0)
            m_NeedBg = Helper.String2Bool((string)param[0]);
    }

    void SetNodeFinder()
    {
        m_Finder = new UINodeFinder();
        m_Finder.SetParam(new object[2]
        {
            "SkipGuidePanel","SkipButton"
        });
        m_TargetButton = m_Finder.FindNode(false);
        m_Finder.SetParam(new object[2]
        {
            "SkipGuidePanel","BgButton"
        });
        m_BgBtn = m_Finder.FindNode(false);
        if (m_TargetButton == null)
        {
            Log.w("=======skipguide uinode is null=======");
        }
    }

    protected override void OnStart()
    {
        //top 打开后将其他的panel变为不可接受射线
        UIMgr.S.topPanelHideMask = PanelHideMask.UnInteractive;
        UIMgr.S.OpenTopPanel(UIID.SkipGuidePanel, null);

        SetNodeFinder();
        AppLoopMgr.S.onUpdate += Update;
    }

    protected override void OnFinish(bool forceClean)
    {
        UIMgr.S.topPanelHideMask = PanelHideMask.None;
        AppLoopMgr.S.onUpdate -= Update;
        UIMgr.S.ClosePanelAsUIID(UIID.SkipGuidePanel);
    }
    private void OnClickUpOnTarget()
    {
        Log.w("=====Click down=====");
        if (m_FinishGuide)
        {
            guideStep.guide.OnStepFinish(guideStep, true);   
        }
        else
        {
            FinishStep();
        }

        ExecuteEvents.Execute<IPointerClickHandler>(m_TargetButton.gameObject, new PointerEventData(UnityEngine.EventSystems.EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    private void OnClickDownOnTarget()
    {
        ExecuteEvents.Execute<IPointerDownHandler>(m_TargetButton.gameObject, new PointerEventData(UnityEngine.EventSystems.EventSystem.current), ExecuteEvents.pointerDownHandler);
    }

    private void Update()
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
            m_HasDown = false;

            if (CheckIsTouchInTarget())
            {
                OnClickUpOnTarget();
            }
        }
    }

    private bool CheckIsTouchInTarget()
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

    private bool IsHitWhiteObject(Transform tr)
    {
        if (tr.IsChildOf(m_TargetButton.transform))
        {
            if (tr == m_TargetButton)
            {
                m_FinishGuide = true;
            }
            return true;
        }
        else
        {
            if (tr == m_BgBtn && m_NeedBg)
                return true;
        }

        return false;
    }
}
