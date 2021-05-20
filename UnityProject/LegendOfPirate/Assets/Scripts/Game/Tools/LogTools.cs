using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using System.Text;

namespace GameWish.Game
{
    public class LogTools : AbstractPanel
    {
        public Button m_LogBtn = null;
        public Text m_ContentText = null;

        private bool m_IsShowing = false;

        private StringBuilder m_Content = new StringBuilder();

        protected override void OnUIInit()
        {
            base.OnUIInit();

            if (AppConfig.S.appMode == APP_MODE.ReleaseMode)
            {
                gameObject.SetActive(false);
                return;
            }

            m_LogBtn.onClick.AddListener(() =>
            {
                if (m_IsShowing)
                {
                    Show(m_IsShowing);
                }
            });
        }

        public void Info(string value)
        {
            //if (m_Content.Length > 50) ;

            m_Content.Append(value);

            m_ContentText.text = m_Content.ToString();
        }

        public void Warning(string value)
        {

        }

        public void Error(string value)
        {

        }

        private void Show(bool show)
        {
            m_ContentText.gameObject.SetActive(show);

            m_IsShowing = !show;

        }
    }
}
