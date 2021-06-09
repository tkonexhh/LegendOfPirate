using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class GardenPanelData : UIPanelData
	{
		public GardenPanelData()
		{
		}
	}
	
	public partial class GardenPanel
	{
		private GardenPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<GardenPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<GardenPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
