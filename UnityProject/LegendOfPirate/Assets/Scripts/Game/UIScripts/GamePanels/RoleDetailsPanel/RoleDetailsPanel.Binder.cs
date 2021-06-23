using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using GFrame.Editor;

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

		private Dictionary<int, SkillItem> m_SkillItemDic = new Dictionary<int, SkillItem>();

		private void AllocatePanelData()
		{
			
		}

		private void OnInit(params object[] args)
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

            RefreshSkillView();

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

           

            for (int i = 0; i < m_RoleModel.skillList.Count; i++)
            {
				m_RoleModel.skillList.ObserveAdd().Subscribe( skillmodel => 
				{
                    m_SkillItemDic[skillmodel.Value.skillId].skillItemTex.text=  "已解锁" ;

                });

			}
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

		private Transform AddSkillItem()
        {
            SoundButton skillBtn = ((GameObject)LoadPageRes("SkillSubpart")).GetComponent<SoundButton>();
            skillBtn.transform.SetParent(SkillRegion);
            skillBtn.onClick.AddListener(() =>
            {
                UIMgr.S.OpenPanel(UIID.RoleSkillPanel, m_RoleModel.id);
            });
            return skillBtn.transform;
		}

		private void RefreshSkillView()
        {
			List<int> skillIdList = new List<int>();
			skillIdList = TDRoleConfigTable.SkillIdDic[m_RoleModel.id];
            if (skillIdList.Count > m_SkillItemDic.Count)
            {
                m_SkillItemDic.Clear();

                for (int i = 0; i < skillIdList.Count; i++)
                {
                    if (!m_SkillItemDic.ContainsKey(skillIdList[i]))
                    {
                        SkillItem skillItem = new SkillItem();
                        skillItem.skillId = skillIdList[i];
                        skillItem.skillItemBtn = SkillRegion.GetChild(i).GetComponent<Button>();
                        skillItem.skillItemTex = skillItem.skillItemBtn.transform.GetChild(0).GetComponent<Text>();
                        m_SkillItemDic.Add(skillIdList[i], skillItem);
                    }
                }
            }
            foreach (var item in m_SkillItemDic.Values)
            {
                item.skillItemTex.text = m_RoleModel.GetSkillModel(item.skillId) == null ? "未解锁" : "已解锁";
            }
        }

        #endregion

		private struct SkillItem
        {
            public int skillId;
			public Button skillItemBtn;
			public Text skillItemTex;

			public SkillItem(int id,Button btn, Text tex)
            {
                skillId = id;
                skillItemBtn = btn;
				skillItemTex = tex;
            }
        }

    }
}
