using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class WarshipUpgradePanelData : UIPanelData
	{
		public BattleShipFleetModel battleShipFleetModel;
		public BattleShipModel battleShipModel;
		public IntReactiveProperty shipIndex;
		public WarshipUpgradePanelData()
		{
		}
	}
	
	public partial class WarshipUpgradePanel
	{
		private WarshipUpgradePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
	     	m_PanelData = UIPanelData.Allocate<WarshipUpgradePanelData>();
			m_PanelData.battleShipFleetModel = ModelMgr.S.GetModel<BattleShipFleetModel>();
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
			m_RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("RightArrowBtn");//TODO
            });
			m_LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("LeftArrowBtn");//TODO
            });
			m_LeftDownExitBtn.OnClickAsObservable().Subscribe(_ =>
            {
				HideSelfWithAnim();
            });
			m_UpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("UpgradeBtn");//TODO
            });
			m_BgBtn.OnClickAsObservable().Subscribe(_ =>
			{
				BgBtnEvent();
			});
		}
		private void UnregisterEvents()
		{

		}
	}
}
