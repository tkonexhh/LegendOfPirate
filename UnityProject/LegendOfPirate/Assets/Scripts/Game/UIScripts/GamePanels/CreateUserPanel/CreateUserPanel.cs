
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public class CreateUserPanel : AbstractPanel
    {
        [SerializeField]
        private TMP_InputField m_NameInputField;
        [SerializeField]
        private TextMeshProUGUI m_TipsText;

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnClose()
        {
            base.OnClose();
        }
        public async void OnCreateClicked()
        {
            string name = m_NameInputField.text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            try
            {
                HeroStorage player = new HeroStorage
                {
                    Name = name,
                    Level = 1,
                    Balance = 0

                };
                LCUser currentUser = await LCUser.GetCurrent();
                currentUser["player"] = player;
                await currentUser.Save();
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.MainMenuPanel);
            }
            catch (LCException e)
            {
                Debug.LogError(e);
                m_TipsText.text = e.ToString();
            }
        }
    }

}