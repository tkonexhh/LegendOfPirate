using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class ToastPanel : AbstractPanel
{
    [SerializeField]
    private Text m_Content = null;

    private bool m_IsInit = false;
    private float m_Time = 0f;

    protected override void OnUIInit()
    {
        base.OnUIInit();

        Init();
    }

    private void Init()
    {

    }

    protected override void OnPanelOpen(params object[] args)
    {
        base.OnPanelOpen(args);
        //OpenDependPanel(EngineUI.MaskPanel,-1);

        string content = args[0] as string;
        m_Content.text = content;

        m_IsInit = true;
        m_Time = 0f;
    }

    private void Update()
    {
        if (m_IsInit)
        {
            m_Time += Time.deltaTime;
            if (m_Time > 1f)
            {
                //HideSelfWithAnim();
                m_IsInit = false;
                CloseSelfPanel();
            }
        }
    }

    protected override void OnClose()
    {
        base.OnClose();

        m_IsInit = false;
        m_Time = 0f;
    }

    //protected override void OnPanelHideComplete()
    //{
    //    base.OnPanelHideComplete();
    //    CloseSelfPanel();
    //}
}
