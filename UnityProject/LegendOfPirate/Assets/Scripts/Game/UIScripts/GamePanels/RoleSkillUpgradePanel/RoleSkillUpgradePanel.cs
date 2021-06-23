using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class RoleSkillUpgradePanel : AbstractAnimPanel
    {
        #region Data
        private int m_RoleID;
        private RoleSkillModel m_RoleSkillModel;
        #endregion
        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            HandleTransmitValue(args);

            BindModelToUI();
            BindUIToModel();
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
        #endregion
        #region Private
        private void HandleTransmitValue(params object[] args)
        {
            m_RoleID = (int)args[0];
            m_RoleSkillModel = args[1] as RoleSkillModel;
        }
        #endregion
    }
}
