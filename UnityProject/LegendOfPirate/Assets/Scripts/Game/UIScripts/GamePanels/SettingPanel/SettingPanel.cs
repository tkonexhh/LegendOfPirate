using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
    public class SettingPanel : AbstractAnimPanel
    {
        [SerializeField]
        private Button m_MusicBtn;
        [SerializeField]
        private GameObject m_MusicOffObj;
        [SerializeField]
        private GameObject m_MusicOnObj;
        [SerializeField]
        private Button m_SoundBtn;
        [SerializeField]
        private GameObject m_SoundOffObj;
        [SerializeField]
        private GameObject m_SoundOnObj;

        [SerializeField]
        private Button m_PrivacyBtn;
        [SerializeField]
        private Button m_CloseBtn;

        [SerializeField]
        private Text m_TitleLan;
        [SerializeField]
        private Text m_YinsiLan;

        private bool isMuiscOn;
        private bool isSoundOn;

        public Text soundText;
        public Text musicText;

        protected override void OnUIInit()
        {
            base.OnUIInit();
            m_MusicBtn.onClick.AddListener(() => { UpdateMusic(!isMuiscOn); });
            m_SoundBtn.onClick.AddListener(() => { UpdateSound(!isSoundOn); });
            m_PrivacyBtn.onClick.AddListener(() => { Application.OpenURL("https://www.modooplay.com/privacy/modooplay.html"); });
            m_CloseBtn.onClick.AddListener(HideSelfWithAnim);

        }

        protected override void OnOpen()
        {
            base.OnOpen();
            OpenDependPanel(EngineUI.MaskPanel, -1, null);
            UpdateMusic(AudioMgr.S.isMusicEnable);
            UpdateSound(AudioMgr.S.isSoundEnable);

            m_TitleLan.text = TDLanguageTable.Get("UI_Setting_Title");
            m_YinsiLan.text = TDLanguageTable.Get("UI_Setting_WWW");

            soundText.text = TDLanguageTable.Get("UI_Setting_Sound");
            musicText.text = TDLanguageTable.Get("UI_Setting_Music");
        }


        private void UpdateMusic(bool ison)
        {
            isMuiscOn = ison;
            AudioMgr.S.isMusicEnable = ison;
            m_MusicOffObj.SetActive(!ison);
            m_MusicOnObj.SetActive(ison);
        }

        private void UpdateSound(bool ison)
        {
            isSoundOn = ison;
            AudioMgr.S.isSoundEnable = ison;
            m_SoundOffObj.SetActive(!ison);
            m_SoundOnObj.SetActive(ison);
        }
        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();
            CloseSelfPanel();
            CloseDependPanel(EngineUI.MaskPanel);
        }

    }
}

