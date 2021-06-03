using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleEquipDetailsPanelData : UIPanelData
	{
		public RoleEquipDetailsPanelData()
		{
		}
	}
	
	public partial class RoleEquipDetailsPanel
	{
		private RoleEquipDetailsPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleEquipDetailsPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleEquipDetailsPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
