using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class RoleDetailsPanelData : UIPanelData
    {
        public RoleModel roleModel;
    }

    public partial class RoleDetailsPanel
    {
        private RoleDetailsPanelData m_PanelData = null;

        private bool m_IsLocked;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<RoleDetailsPanelData>();
        }

        #region RefreshPanelData
        private void ReleasePanelData()
        {
            ObjectPool<RoleDetailsPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.roleModel.level.SubscribeToTextMeshPro(m_RoleLevel, "Lv:{0}");
            m_PanelData.roleModel.level.SubscribeToTextMeshPro(m_ExperienceValue, "{0}/999");
            m_PanelData.roleModel.level.Subscribe(value =>
           {
               m_ExperienceBar.fillAmount = (float)value / 999f;
           });
        }

        private void BindUIToModel()
        {

        }
        #endregion

        #region RefreshPanelView
        private void RefreshRoleIsUnclockView(bool isUnlock)
        {
            m_StartRegion.gameObject.SetActive(isUnlock);
            m_RoleLevel.gameObject.SetActive(isUnlock);
            m_ExperienceBar.gameObject.SetActive(isUnlock);
            m_EquipRegion.gameObject.SetActive(isUnlock);
        }

        private void RefreshRoleView()
        {

        }

        private void AddSkillItem()
        {
            SoundButton skillBtn = ((GameObject)LoadPageRes("SkillSubpart")).GetComponent<SoundButton>();
            skillBtn.transform.SetParent(m_SkillRegion);
            skillBtn.onClick.AddListener(() =>
            {
                UIMgr.S.OpenPanel(UIID.RoleSkillUpgradePanel, m_PanelData.roleModel.id);
            });
        }
        #endregion
    }
}
