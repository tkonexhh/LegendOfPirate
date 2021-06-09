using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class BuildingLevelUpPanelData : UIPanelData
	{
		public BuildingLevelUpPanelData()
		{
		}
	}
	
	public partial class BuildingLevelUpPanel
	{
		private BuildingLevelUpPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<BuildingLevelUpPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<BuildingLevelUpPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
