using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class MainMenuPanel : AbstractAnimPanel
    {
        protected override void OnUIInit()
        {
            base.OnUIInit();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            AllocatePanelData(args);

            BindModelToUI();
            BindUIToModel();
            OnClickAddListener();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
        }

        #region  OnClickAddListener

        private void OnClickAddListener()
        {
            RoleBtn.OnClickAsObservable().Subscribe(_ => { OpenRolePanel(); }).AddTo(this);
            StorageBtn.OnClickAsObservable().Subscribe(_ => { OpenWareHousePanel(); }).AddTo(this);
        }

        private void OpenRolePanel()
        {
            UIMgr.S.OpenPanel(UIID.RoleGroupPanel);
        }
        private void OpenWareHousePanel()
        {
            UIMgr.S.OpenPanel(UIID.WareHousePanel);
        }
        #endregion
    }
}
