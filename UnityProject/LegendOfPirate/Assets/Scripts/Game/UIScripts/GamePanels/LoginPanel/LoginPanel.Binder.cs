using System.Threading.Tasks;
using LeanCloud.Storage;
using Qarth.Extension;
using UnityEngine;
using LeanCloud;
using Qarth;
using UniRx;


namespace GameWish.Game
{
    public class LoginPanelData : UIPanelData
    {
        public LoginPanelData()
        {
        }
    }

    public partial class LoginPanel
    {
        private LoginPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<LoginPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<LoginPanelData>.S.Recycle(m_PanelData);
        }

        private void OnClickAddListener()
        {
            RegisterBtn.OnClickAsObservable().Subscribe(_ => OnRegisterClicked()).AddTo(this);
            BackBtn.OnClickAsObservable().Subscribe(_ => OnBackClicked()).AddTo(this);
            OpenRegisterBtn.OnClickAsObservable().Subscribe(_ => OnRegisterClicked()).AddTo(this);
            LoginBtn.OnClickAsObservable().Subscribe(_ => OnLoginClicked()).AddTo(this);
        }

        private async void BindModelToUI()
        {
            SetBtnsStates(false);
            string sessionToken = PlayerPrefs.GetString("token");
            if (!string.IsNullOrEmpty(sessionToken))
            {
                try
                {
                    Root.gameObject.SetActive(false);
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
                Root.gameObject.SetActive(true);
            }
        }

        private void BindUIToModel()
        {
        }

        public void OnOpenRegister()
        {
            SetBtnsStates(true);
        }

        private void SetBtnsStates(bool flag)
        {
            UsernameInputField.text = "";
            PasswordInputField.text = "";
            RegisterRoot.gameObject.SetActive(flag);
            LoginRoot.gameObject.SetActive(!flag);
        }

        public async void OnLoginClicked()
        {
            string username = UsernameInputField.text;
            if (string.IsNullOrEmpty(username))
            {
                TipsText.text = "??????????????????";
                return;
            }
            string password = PasswordInputField.text;
            if (string.IsNullOrEmpty(password))
            {
                TipsText.text = "??????????????????";
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
                TipsText.text = e.ToString();
            }
        }

        public void OnBackClicked()
        {
            SetBtnsStates(false);
        }

        public async void OnRegisterClicked()
        {
            string username = UsernameInputField.text;
            if (string.IsNullOrEmpty(username))
            {
                TipsText.text = "??????????????????";
                return;
            }
            string password = PasswordInputField.text;
            if (string.IsNullOrEmpty(password))
            {
                TipsText.text = "??????????????????";
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
                // ????????????
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.CreateUserPanel);
            }
            catch (LCException e)
            {
                Debug.LogError(e);
                TipsText.text = e.ToString();
            }
        }

        private async Task OnLogin(LCUser user)
        {
            CloseSelfPanel();
            if (user["player"] != null)
            {
                await user.Fetch(includes: new string[] { "player" });
                UserStorage currentUser = user["player"] as UserStorage;
                Debug.Log($"??????<b>{currentUser.Name}</b>?????? ????????????");
                UIMgr.S.OpenPanel(UIID.MainMenuPanel);
            }
            else
            {
                UIMgr.S.OpenPanel(UIID.CreateUserPanel);
            }
        }
    }
}

