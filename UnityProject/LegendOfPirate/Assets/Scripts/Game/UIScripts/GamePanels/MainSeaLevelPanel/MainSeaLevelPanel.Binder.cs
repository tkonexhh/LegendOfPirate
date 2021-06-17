using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class MainSeaLevelPanelData : UIPanelData
	{
		public MainSeaLevelPanelData()
		{
		}
	}
	
	public partial class MainSeaLevelPanel
	{
		private MainSeaLevelPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<MainSeaLevelPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<MainSeaLevelPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
