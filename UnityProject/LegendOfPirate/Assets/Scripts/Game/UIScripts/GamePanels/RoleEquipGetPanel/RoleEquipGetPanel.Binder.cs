using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleEquipGetPanelData : UIPanelData
	{
		public IInventoryItemModel inventoryItemModel;
		public int equipId;
		public int roleId;
		public EquipmentType equipmentType;
		public RoleEquipGetPanelData()
		{
		}
	}
	
	public partial class RoleEquipGetPanel
	{
		private RoleEquipGetPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<RoleEquipGetPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleEquipGetPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
