using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class FishingPanelData : UIPanelData
	{
		
		public FishingPanelData()
        {

        }
	}
	
	public partial class FishingPanel
	{
		private FishingPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<FishingPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<FishingPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
