using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class AchievementTaskPanelData : UIPanelData
	{
		public AchievementTaskPanelData()
		{
		}
	}
	
	public partial class AchievementTaskPanel
	{
		private AchievementTaskPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<AchievementTaskPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<AchievementTaskPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
