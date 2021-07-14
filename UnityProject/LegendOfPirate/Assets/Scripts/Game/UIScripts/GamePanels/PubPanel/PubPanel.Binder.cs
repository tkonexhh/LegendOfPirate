using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class PubPanelData : UIPanelData
	{
		public PubPanelData()
		{
		}
	}
	
	public partial class PubPanel
	{
		private PubPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<PubPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<PubPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
