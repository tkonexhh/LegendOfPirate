using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleGetPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();
            OnClickAddListener();
        }
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			if (args != null & args.Length > 0)
				GetRoleModel((int)args[0]);

			InitPanelMsg();

			InitListView();

			
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

		private void InitListView() 
		{
			m_RoleSkills.SetCellRenderer(OnRoleSkillCellRenderer);
			m_RoleSkills.SetDataCount(m_PanelData.roleModel.skillList.Count);
		}

        private void OnRoleSkillCellRenderer(Transform root, int index)
        {
			//设置技能图标
			//root.GetComponent<Image>().sprite = SpriteHandler.S.GetSprite();
			root.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => UIMgr.S.OpenPanel(UIID.RoleSkillUpgradePanel, m_PanelData.skillId[index])).AddTo(this);
		}
    }
}
