using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleEquipDetailsPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			
			AllocatePanelData();

			InitializePassValue(args);

			GetInformationForNeed();
			
			BindModelToUI();
			BindUIToModel();
		}

        private void InitializePassValue(object[] args)
        {
            m_PanelData.roleID = (int)args[0];
            m_PanelData.roleEquipID = (int)args[1];
        }

        private void GetInformationForNeed()
		{
            m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            m_PanelData.roleModel = m_PanelData.roleGroupModel.GetRoleModel(m_PanelData.roleID);
            m_PanelData.roleEquipModel = m_PanelData.roleModel.GetEquipModel(m_PanelData.roleEquipID);
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
		
	}
}
