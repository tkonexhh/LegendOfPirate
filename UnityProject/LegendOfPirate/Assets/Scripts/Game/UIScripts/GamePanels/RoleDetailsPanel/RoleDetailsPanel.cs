using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public partial class RoleDetailsPanel : AbstractAnimPanel
    {
        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            RegisterEvents();

            HandleTransmitValue(args);

            BindModelToUI();
            BindUIToModel();

            InitData();
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
            UnregisterEvents();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            StoryBtn.onClick.AddListener(() =>
            {
                UIMgr.S.OpenPanel(UIID.RoleStoryPanel, m_PanelData.roleModel.id);
            });
        }
        #endregion

        #region EventSystem
        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }
        #endregion

        #region Ohter Method

        private void HandleTransmitValue(params object[] args)
        {
            RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            m_PanelData.roleModel = roleGroupModel.GetRoleModel(1001);
            //if (args != null && args.Length > 0)
            //{
            //    if (args.Length >= 1)
            //    {
            //        RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            //        m_PanelData.roleModel = (RoleModel)args[0];
            //    }
            //}

            RoleName.text = m_PanelData.roleModel.name;
            m_IsLocked = m_PanelData.roleModel.isLocked.Value;

            if (!m_IsLocked)
            {
                RefreshRoleIsUnclockView(m_IsLocked);
                return;
            }
        }

        private void InitData()
        {
            InitRoleSkillsData();
        }

        private void InitRoleSkillsData()
        {

        }
        #endregion
    }
}
