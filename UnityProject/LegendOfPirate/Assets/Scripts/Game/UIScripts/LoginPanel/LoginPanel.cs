using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public class LoginPanel : AbstractPanel

    {
        [SerializeField]
        private GameObject m_RootObjects;
        [SerializeField]
        private GameObject m_LoginBtns;
        [SerializeField]
        private GameObject m_RegisterBtns;
        [SerializeField]
        private TMP_InputField m_UsernameInputField;
        [SerializeField]
        private TMP_InputField m_PasswordInputField;
        [SerializeField]
        private TextMeshProUGUI m_TipsText;

        protected override async void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            SetBtnsStates(false);

            string sessionToken = PlayerPrefs.GetString("token");
            if (!string.IsNullOrEmpty(sessionToken))
            {
                try
                {
                    m_RootObjects.SetActive(false);
                    LCUser currentUser = await LCUser.BecomeWithSessionToken(sessionToken);
                    await OnLogin(currentUser);
                }
                catch (LCException e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                m_RootObjects.SetActive(true);
            }
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnClose()
        {
            base.OnClose();
        }
        private void SetBtnsStates(bool flag)
        {
            m_RegisterBtns.SetActive(flag);
            m_LoginBtns.SetActive(!flag);
        }
        public void OnOpenRegister()
        {
            SetBtnsStates(true);
        }

        public async void OnLoginClicked()
        {
            string username = m_UsernameInputField.text;
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            string password = m_PasswordInputField.text;
            if (string.IsNullOrEmpty(password))
            {
                return;
            }

            try
            {
                LCUser currentUser = await LCUser.Login(username, password);
                PlayerPrefs.SetString("token", currentUser.SessionToken);
                await OnLogin(currentUser);
            }
            catch (LCException e)
            {
                Debug.LogError(e);
                m_TipsText.text = e.ToString();
            }
        }
        public void OnBackClicked()
        {
            SetBtnsStates(false);
        }

        public async void OnRegisterClicked()
        {
            string username = m_UsernameInputField.text;
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            string password = m_PasswordInputField.text;
            if (string.IsNullOrEmpty(password))
            {
                return;
            }

            LCUser user = new LCUser
            {
                Username = username,
                Password = password
            };
            try
            {
                LCUser currentUser = await user.SignUp();
                PlayerPrefs.SetString("token", currentUser.SessionToken);
                // 注册成功
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.CreateHeroPanel);
            }
            catch (LCException e)
            {
                Debug.LogError(e);
                m_TipsText.text = e.ToString();
            }
        }

        private async Task OnLogin(LCUser user)
        {
            CloseSelfPanel();
            if (user["player"] != null)
            {
                await user.Fetch(includes: new string[] { "player" });
                HeroStorage hero = user["player"] as HeroStorage;
                Debug.Log($"欢迎<b>{hero.Name}</b>来到 海盗世界");
                UIMgr.S.OpenPanel(UIID.MainMenuPanel);
            }
            else
            {
                UIMgr.S.OpenPanel(UIID.CreateHeroPanel);
            }
        }
    }

}