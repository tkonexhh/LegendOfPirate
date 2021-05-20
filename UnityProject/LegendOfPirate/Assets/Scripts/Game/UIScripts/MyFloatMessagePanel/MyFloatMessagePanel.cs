//  Desc:        Framework For Game Develop with Unity3d
//  Copyright:   Copyright (C) 2017 SnowCold. All rights reserved.
//  WebSite:     https://github.com/SnowCold/Qarth
//  Blog:        http://blog.csdn.net/snowcoldgame
//  Author:      SnowCold
//  E-mail:      snowcold.ouyang@gmail.com
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using Qarth;

public class MyFloatMessagePanel : AbstractPanel
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private Vector3 m_EndPos;
    [SerializeField] private float m_AnimTime = 1.0f;

    private Text m_ValueText;
    private Color m_InitColor;
    private CanvasGroup m_CanvasGroup;
    private Sequence m_Sequence;

    public override int sortIndex
    {
        get
        {
            return UIRoot.FLOAT_PANEL_INDEX;
        }
    }

    protected override void OnUIInit()
    {
        m_ValueText = m_Prefab.GetComponentInChildren<Text>();
        m_CanvasGroup = m_Prefab.GetComponent<CanvasGroup>();
        m_InitColor = m_ValueText.color;
        InitFloatMessage();

        m_Sequence = DOTween.Sequence();
        m_Sequence.Append(m_Prefab.transform.DOLocalMove(m_EndPos, m_AnimTime).SetEase(Ease.Linear));
        m_Sequence.Append(m_CanvasGroup.DOFade(0, m_AnimTime).SetEase(Ease.Linear));
        m_Sequence.SetAutoKill(false);
        m_Sequence.Pause();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }

    protected override void OnPanelOpen(params object[] args)
    {

    }

    public void ShowMsg(string msg)
    {
        if (msg == null)
        {
            return;
        }

        PlayFloatMessage(msg);
    }

    public void PlayFloatMessage(string msg)
    {
        if (UIMgr.isApplicationQuit)
        {
            return;
        }
        InitFloatMessage();

        if (m_Prefab)
        {
            m_Prefab.SetActive(true);
            m_ValueText.text = msg;
            m_Sequence.Restart();
        }
    }

    private void InitFloatMessage()
    {
        m_Prefab.SetActive(false);
        m_ValueText.color = m_InitColor;
    }

    private void Update()
    {

    }
}
