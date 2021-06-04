using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class EquipOnPanelData : UIPanelData
	{
		public EquipOnPanelData()
		{
		}
	}
	
	public partial class EquipOnPanel
	{
		private EquipOnPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<EquipOnPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<EquipOnPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
