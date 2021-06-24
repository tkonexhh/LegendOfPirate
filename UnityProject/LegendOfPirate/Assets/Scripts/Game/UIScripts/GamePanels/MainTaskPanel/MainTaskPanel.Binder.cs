using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class MainTaskPanelData : UIPanelData
    {
        public MainTaskModel mainTaskModel;
        public MainTaskPanelData()
        {
        }
    }

    public partial class MainTaskPanel
    {
        private MainTaskPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<MainTaskPanelData>();
            m_PanelData.mainTaskModel = new MainTaskModel();
        }

        private void ReleasePanelData()
        {
            ObjectPool<MainTaskPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.mainTaskModel.titleTex.SubscribeToTextMeshPro(m_TitleTex).AddTo(this);
            m_PanelData.mainTaskModel.contentTex.SubscribeToTextMeshPro(m_ContentTex).AddTo(this);
            m_PanelData.mainTaskModel.rewardTex.SubscribeToTextMeshPro(m_RewardNumTex).AddTo(this);
            m_PanelData.mainTaskModel.claimTex.SubscribeToTextMeshPro(m_ClaimTex).AddTo(this);
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
            m_BackBtn.OnClickAsObservable().Subscribe(_ => OnBackClicked()).AddTo(this);
            m_ClaimBtn.OnClickAsObservable().Subscribe(_ => OnClaimClicked()).AddTo(this);
        }
        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainMenuPanel);
        }
        private void OnClaimClicked()
        {
            switch (m_PanelData.mainTaskModel.curMainTaskRewardType)
            {
                case MainTaskModel.MainTaskRewardType.Finished:
                    Debug.Log("task have finish");
                    m_PanelData.mainTaskModel.ResetTaskState();
                    break;
                case MainTaskModel.MainTaskRewardType.UnFinished:
                    Debug.Log("task have UnFinished");
                    break;
            }
        }
        private void InitPanelData()
        {
            UpdateTasks(0, m_PanelData.mainTaskModel.GetCurTaskID());
        }
        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.MainTaskRefresh, UpdateTasks);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.Register(EventID.MainTaskRefresh, UpdateTasks);
        }
        private void UpdateTasks(int key, params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                int taskID = (int)args[0];
                m_PanelData.mainTaskModel.RefreshData(taskID);
            }
        }
    }
}
