using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleGroupPanelData : UIPanelData
	{
		public RoleGroupPanelData()
		{
		}
	}
	
	public partial class RoleGroupPanel
	{
		private RoleGroupPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<RoleGroupPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleGroupPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
