using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class ForgePanelData : UIPanelData
	{
		public ForgeModel forgeModel;
		public ForgePanelData()
		{
		}
	}
	
	public partial class ForgePanel
	{
		private ForgePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<ForgePanelData>();
			
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<ForgePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
