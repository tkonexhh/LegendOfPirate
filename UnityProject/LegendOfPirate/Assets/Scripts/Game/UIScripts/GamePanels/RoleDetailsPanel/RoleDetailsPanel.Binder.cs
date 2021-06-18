using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleDetailsPanelData : UIPanelData
	{

	}
	
	public partial class RoleDetailsPanel
	{
		private RoleDetailsPanelData m_PanelData = null;
		private RoleModel m_RoleModel;
		private bool m_IsUnlock;

		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<RoleDetailsPanelData>();
            if (args != null && args.Length > 0)
            {
                if (args.Length >= 1)
                {
					m_RoleModel = (RoleModel)args[0];
                }
            }

			RoleName.text = m_RoleModel.name;
			m_IsUnlock = m_RoleModel.isUnlock.Value;

			if (!m_IsUnlock)
            {
				RefreshRoleIsUnclockView(m_IsUnlock);
				return;
            }

			//RegionRoleName.text		

		}

        #region RefreshPanelData

        private void ReleasePanelData()
		{
			ObjectPool<RoleDetailsPanelData>.S.Recycle(m_PanelData);
			
		}
		
		private void BindModelToUI()
		{
			m_RoleModel.level.SubscribeToTextMeshPro(RoleLevel,"Lv:{0}");
			m_RoleModel.level.SubscribeToTextMeshPro(ExperienceValue, "{0}/999");
			m_RoleModel.level.Subscribe( value => 
			{
				ExperienceBar.fillAmount = (float)value / 999f;
			});
			
		}
		
		private void BindUIToModel()
		{

		}


        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }

		private void OnClickAddListener()
        {
			StoryBtn.onClick.AddListener(() => 
			{
				UIMgr.S.OpenPanel(UIID.RoleStoryPanel,m_RoleModel.id);
			});
			//LeftRoleBtn.OnClickAsObservable().Subscribe();
			//RightRoleBtn.OnClickAsObservable().Subscribe();


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
                UIMgr.S.OpenPanel(UIID.RoleSkillPanel, m_RoleModel.id);
            });

		}



        #endregion

    }
}
