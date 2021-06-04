using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleDetailsPanelData : UIPanelData
	{
		public RoleModel roleModel { get; private set; }
		public RoleDetailsPanelData()
		{
			roleModel = ModelMgr.S.GetModel<RoleModel>();
		}
	}
	
	public partial class RoleDetailsPanel
	{
		private RoleDetailsPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleDetailsPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleDetailsPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			//m_PanelData.roleModel.curExp.SubscribeToText(ExperienceBar);
		}
		
		private void BindUIToModel()
		{

		}
		
	}
}
