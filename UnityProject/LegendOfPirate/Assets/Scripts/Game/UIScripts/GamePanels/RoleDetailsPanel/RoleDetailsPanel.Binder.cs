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
            m_PanelData.roleModel.level.SubscribeToTextMeshPro(RoleLevel, "Lv:{0}");
            m_PanelData.roleModel.level.SubscribeToTextMeshPro(ExperienceValue, "{0}/999");
            m_PanelData.roleModel.level.Subscribe(value =>
           {
               ExperienceBar.fillAmount = (float)value / 999f;
           });
        }

        private void BindUIToModel()
        {

        }
        #endregion

        #region RefreshPanelView
        private void RefreshRoleIsUnclockView(bool isUnlock)
        {
            StartRegion.gameObject.SetActive(isUnlock);
            RoleLevel.gameObject.SetActive(isUnlock);
            ExperienceBar.gameObject.SetActive(isUnlock);
            EquipRegion.gameObject.SetActive(isUnlock);
        }

        private void RefreshRoleView()
        {

        }

        private void AddSkillItem()
        {
            SoundButton skillBtn = ((GameObject)LoadPageRes("SkillSubpart")).GetComponent<SoundButton>();
            skillBtn.transform.SetParent(SkillRegion);
            skillBtn.onClick.AddListener(() =>
            {
                UIMgr.S.OpenPanel(UIID.RoleSkillPanel, m_PanelData.roleModel.id);
            });
        }
        #endregion
    }
}
