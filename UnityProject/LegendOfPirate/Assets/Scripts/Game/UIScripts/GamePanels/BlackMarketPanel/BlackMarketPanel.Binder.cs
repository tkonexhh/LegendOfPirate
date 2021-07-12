using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class BlackMarketPanelData : UIPanelData
	{
		public BlackMarketModel blackMarketModel; 
		public BlackMarketPanelData()
		{
		}
	}
	
	public partial class BlackMarketPanel
	{
		private BlackMarketPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<BlackMarketPanelData>();

			m_PanelData.blackMarketModel = ModelMgr.S.GetModel<BlackMarketModel>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<BlackMarketPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_PanelData.blackMarketModel.RefreshNeedDiamonds.SubscribeToTextMeshPro(m_RefreshDiamonds).AddTo(this);
			m_PanelData.blackMarketModel.refreshCommodityCountDown.SubscribeToTextMeshPro(m_RefreshCountDown).AddTo(this);
			m_PanelData.blackMarketModel.BlackMarketCommoditys.ObserveCountChanged().Subscribe(count=> MonitoringDataList(count)).AddTo(this);
		}
		
		private void BindUIToModel()
		{
		}
	}
}
