using System.Threading.Tasks;
using LeanCloud.Storage;
using Qarth.Extension;
using UnityEngine;
using LeanCloud;
using Qarth;
using UniRx;


namespace GameWish.Game
{
    public class CreateUserPanelData : UIPanelData
    {
        public CreateUserPanelData()
        {
        }
    }

    public partial class CreateUserPanel
    {
        private CreateUserPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<CreateUserPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<CreateUserPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
            CreateBtn.OnClickAsObservable().Subscribe(_ => OnCreateClicked()).AddTo(this);
        }
        public async void OnCreateClicked()
        {
            string name = NameInputField.text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            try
            {
                bool hasUser = await StorageHander.S.QueryUserName(name);
                if (!hasUser)
                {
                    UserStorage player = new UserStorage
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
                else
                {
                    TipsText.text = "该角色名已存在,请重新输入!";
                }

            }
            catch (LCException e)
            {
                Debug.LogError(e);
                TipsText.text = e.ToString();
            }
        }

    }
}
