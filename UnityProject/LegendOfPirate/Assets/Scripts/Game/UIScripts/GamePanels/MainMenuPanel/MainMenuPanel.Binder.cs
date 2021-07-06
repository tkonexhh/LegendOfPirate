using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class MainMenuPanelData : UIPanelData
    {
        public MainTaskModel mainTaskModel;
        public MainMenuPanelData()
        {
        }
    }

    public partial class MainMenuPanel
    {
        private MainMenuPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<MainMenuPanelData>();
            m_PanelData.mainTaskModel = new MainTaskModel();
        }

        private void ReleasePanelData()
        {
            ObjectPool<MainMenuPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.mainTaskModel.titleTex.SubscribeToTextMeshPro(m_TaskTex).AddTo(this);
        }

        private void BindUIToModel()
        {
        }

        private void OnClickAddListener()
        {
            m_RoleBtn.OnClickAsObservable().Subscribe(_ => { OpenRolePanel(); }).AddTo(this);
            m_StorageBtn.OnClickAsObservable().Subscribe(_ => { OpenWareHousePanel(); }).AddTo(this);
            m_TaskInfoBgBtn.OnClickAsObservable().Subscribe(_ => { OpenMainTaskPanel(); }).AddTo(this);
            m_MapBtn.OnClickAsObservable().Subscribe(_ => { OpenMainSealevelPanel(); }).AddTo(this);
            m_ShopBtn.OnClickAsObservable().Subscribe(_ => { OpenChargingInterfacePanel(); }).AddTo(this);
        }
        private void InitPanelData()
        {
            UpdateTasksTitle(0, m_PanelData.mainTaskModel.GetCurTaskID());
        }
        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.MainTaskRefresh, UpdateTasksTitle);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.Register(EventID.MainTaskRefresh, UpdateTasksTitle);
        }
        private void OpenRolePanel()
        {
            UIMgr.S.OpenPanel(UIID.RoleGroupPanel);
            //UIMgr.S.OpenPanel(UIID.RoleDetailsPanel);
        }
        private void OpenWareHousePanel()
        {
            UIMgr.S.OpenPanel(UIID.WareHousePanel);
        }
        private void OpenMainSealevelPanel()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainSeaLevelPanel);
        }
        private void OpenChargingInterfacePanel()
        {
            UIMgr.S.OpenPanel(UIID.ChargingInterfacePanel);
        }
        private void OpenMainTaskPanel()
        {
            UIMgr.S.OpenPanel(UIID.MainTaskPanel);
        }

        private void UpdateTasksTitle(int key, params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                int taskID = (int)args[0];
                m_PanelData.mainTaskModel.RefreshTitle(taskID);
                m_TaskTipsImg.gameObject.SetActive(m_PanelData.mainTaskModel.IsFinishTask());
            }
        }
    }
}
