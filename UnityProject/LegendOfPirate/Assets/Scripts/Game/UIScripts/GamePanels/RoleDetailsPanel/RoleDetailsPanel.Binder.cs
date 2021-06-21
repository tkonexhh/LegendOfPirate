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
		private bool m_IsLocked;

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
            m_IsLocked = m_RoleModel.isLocked.Value;
			Log.e(m_IsLocked);

			if (!m_IsLocked)
            {
				RefreshRoleIsUnclockView(m_IsLocked);
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
			m_RoleModel.spiritCount.SubscribeToTextMeshPro(UpgradeMaterialsValue, "{0}/200");
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
			CloseBtn.onClick.AddListener(() =>
			{
				CloseSelfPanel();
			});

			UpgradeMaterialsBtn.onClick.AddListener(() =>
			{
                if (m_RoleModel.spiritCount.Value >= 200)
                {
					UIMgr.S.OpenPanel(UIID.RoleGetPanel);
                    if (!m_RoleModel.isLocked.Value)
                    {
						ModelMgr.S.GetModel<RoleGroupModel>().SetRoleUnlockedModel(m_RoleModel.id);
						RefreshRoleIsUnclockView(true);
					}
                }
                else
                {
					Log.e("Not enough spirit ");
                }
			});

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
