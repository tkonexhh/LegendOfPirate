using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;

public class ClosePanelMask : TipsBehaviour {

    [SerializeField]
    private bool m_CheckOnlyTop = true;
    [SerializeField]
    private bool m_UseDoozyUI = false;
    [SerializeField]
    private float m_Delay = 1;

    private AbstractPanel m_Panel;

    private Action m_Listener;
    private int m_PanelEventFrame = 0;
    private DateTime time;

	// Use this for initialization
	void Start () {
		
	    if (m_Panel == null)
	    {
	        m_Panel = gameObject.GetComponent<AbstractPanel>();
	    }

	    EventSystem.S.UnRegister(EngineEventID.OnPanelUpdate, OnPanelUpdate);

	    time = DateTime.Now;

	}
    private void OnPanelUpdate(int key, params object[] args)
    {
        m_PanelEventFrame = Time.frameCount;
    }

    public void SetOnClickEmptyListener(Action l)
    {
        m_Listener = l;
    }

    protected override void OnClickEmptyArea()
    {
        if (m_PanelEventFrame == Time.frameCount||DateTime.Now<time.AddSeconds(m_Delay))
        {
            return;
        }

        if (m_Panel != null)
        {
            if(m_Panel.hasOpen)
            {
                if (m_CheckOnlyTop)
                {
                    if (m_Panel.uiID != UIMgr.S.FindTopPanel<int>())
                    {
                        return;
                    }
                }

                if (Time.frameCount > m_Panel.lastOpenFrame)
                {
                    if (m_Listener != null)
                    {
                        m_Listener();
                    }
                    else
                    {
                        if (m_UseDoozyUI)
                        {
                            ((AbstractAnimPanel)m_Panel).HideSelfWithAnim();
                        }
                        else
                        {
                            UIMgr.S.ClosePanel(m_Panel);
                        }    
                    }
                }
            }
        }
    }
}
