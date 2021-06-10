using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class LandUpgradePanelData : UIPanelData
	{
		public LandUpgradePanelData()
		{
		}
	}
	
	public partial class LandUpgradePanel
	{
		private LandUpgradePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<LandUpgradePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<LandUpgradePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
