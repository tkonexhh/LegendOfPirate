using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class LogoPanel : AbstractPanel
    {
        [SerializeField]
        private float m_ShowTime = 1;
        [SerializeField]
        private Image m_ProgressBar = null;
        [SerializeField]
        private Image m_BgImg = null;

        private Action m_Listener;
        private int m_TimeID;

        [SerializeField] private Image m_LoadingIcon;

        private float m_Percent = 0f;

        protected override void OnPanelOpen(params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                m_Listener = args[0] as Action;
            }

            if (m_TimeID > 0)
            {
                Timer.S.Cancel(m_TimeID);
            }

            if (I18Mgr.S.language == SystemLanguage.Chinese || I18Mgr.S.language == SystemLanguage.ChineseSimplified)
            {
                m_BgImg.sprite = Resources.Load<Sprite>("UI/Panels/LogoPanel/Logo_Cn");
            }
            else
            {
                m_BgImg.sprite = Resources.Load<Sprite>("UI/Panels/LogoPanel/Logo_En");
            }

            m_TimeID = Timer.S.Post2Really(OnTimeReach, m_ShowTime);

            m_ProgressBar.fillAmount = 0f;
            EventSystem.S.Register(EventID.OnUpdateLoadProgress, HandleEvent);
        }

        private void OnTimeReach(int count)
        {
            m_TimeID = -1;

            if (m_Listener != null)
            {
                m_Listener();
                m_Listener = null;
            }
        }

        protected override void OnClose()
        {
            base.OnClose();

            EventSystem.S.UnRegister(EventID.OnUpdateLoadProgress, HandleEvent);
        }

        void Update()
        {
            //m_LoadingIcon.transform.Rotate(Vector3.back,45*Time.deltaTime);
        }

        private void HandleEvent(int key, params object[] param)
        {
            if (key == (int)EventID.OnUpdateLoadProgress)
            {
                float value = (float)param[0];
                m_Percent += value;
                m_Percent = Mathf.Clamp01(m_Percent);
                m_ProgressBar.fillAmount = m_Percent;
            }
        }
    }
}
