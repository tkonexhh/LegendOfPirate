
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleFragmentsPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
			
			AllocatePanelData();
			
			BindModelToUI();
			
			BindUIToModel();
			
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			InitPanelData(args);
			InitPanelMsg();
		}

        private void InitPanelData(object[] args)
        {
			m_PanelData.roleId = (int)args[0];
			m_PanelData.roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(m_PanelData.roleId);
		
        }

		private void InitPanelMsg() 
		{
		//SetIcon

		}

        protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
		}
		
		protected override void BeforDestroy()
		{
			base.BeforDestroy();
			
			ReleasePanelData();
		}
		
	}
}
