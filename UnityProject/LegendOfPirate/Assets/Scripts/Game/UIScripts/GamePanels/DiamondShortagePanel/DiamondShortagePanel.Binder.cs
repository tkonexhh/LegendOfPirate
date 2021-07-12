using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class DiamondShortagePanelData : UIPanelData
	{
		public DiamondShortagePanelData()
		{
		}
	}
	
	public partial class DiamondShortagePanel
	{
		private DiamondShortagePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<DiamondShortagePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<DiamondShortagePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
