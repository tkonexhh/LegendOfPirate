using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class WarshipUpgradePanelData : UIPanelData
	{
		public WarshipUpgradePanelData()
		{
		}
	}
	
	public partial class WarshipUpgradePanel
	{
		private WarshipUpgradePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<WarshipUpgradePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<WarshipUpgradePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		private void RegisterEvents()
		{

		}

		private void OnClickAddListener()
		{
			RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("RightArrowBtn");//TODO
            });
			LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("LeftArrowBtn");//TODO
            });
			LeftDownExitBtn.OnClickAsObservable().Subscribe(_ =>
            {
				HideSelfWithAnim();
            });
			UpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("UpgradeBtn");//TODO
            });
        }
		private void UnregisterEvents()
		{

		}
	}
}
