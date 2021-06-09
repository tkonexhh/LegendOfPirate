using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class KitchenPanelData : UIPanelData
	{
		public KitchenPanelData()
		{
		}
	}
	
	public partial class KitchenPanel
	{
		private KitchenPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<KitchenPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<KitchenPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
