using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class BattleShipPanelData : UIPanelData
	{
		public BattleShipFleetModel battleshipFleetModel;
		public BattleShipPanelData()
		{
		}
	}
	
	public partial class BattleShipPanel
	{
		private BattleShipPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<BattleShipPanelData>();
			m_PanelData.battleshipFleetModel = ModelMgr.S.GetModel<BattleShipFleetModel>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<BattleShipPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
