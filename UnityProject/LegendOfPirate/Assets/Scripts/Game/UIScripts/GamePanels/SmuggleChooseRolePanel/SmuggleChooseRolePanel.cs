using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class SmuggleChooseRolePanel : AbstractAnimPanel
	{
		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
			AllocatePanelData();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			InitData(args);

			InitPanelMsg();

			BindModelToUI();
			BindUIToModel();


			OnClickAddListener();
		}

        private void InitData(params object[] args) 
		{
			m_PanelData.orderModel = ModelMgr.S.GetModel<SmuggleModel>().GetSmuggleOrderModel((int)args[0]);
			m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
		}

        private void InitPanelMsg()
        {

			m_SelectRoleItems = m_WarShipSelectedRegion.GetComponentsInChildren<Transform>();
			m_AllRoleList.SetCellRenderer(OnRoleListChange);
			m_AllRoleList.SetDataCount(m_PanelData.roleGroupModel.roleUnlockedItemList.Count);
		}

        private void OnRoleListChange(Transform root, int index)
        {

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
		#region Button Event
		private void ExitBtnEvent()
		{
			HideSelfWithAnim();
		}
		#endregion
	}
}
