using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleFragmentsPanelData : UIPanelData
	{
		public RoleModel roleModel;
		public int roleId;
		public int needCount;
		public RoleFragmentsPanelData()
		{
		}
	}
	
	public partial class RoleFragmentsPanel
	{
		private RoleFragmentsPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<RoleFragmentsPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleFragmentsPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
