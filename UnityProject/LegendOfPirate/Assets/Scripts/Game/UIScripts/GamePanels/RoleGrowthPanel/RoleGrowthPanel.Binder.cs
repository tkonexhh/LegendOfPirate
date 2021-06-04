using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleGrowthPanelData : UIPanelData
	{
		public RoleGrowthPanelData()
		{
		}
	}
	
	public partial class RoleGrowthPanel
	{
		private RoleGrowthPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleGrowthPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleGrowthPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
