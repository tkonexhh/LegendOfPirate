using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class CommoditySellPanelData : UIPanelData
	{
		public CommoditySellPanelData()
		{
		}
	}
	
	public partial class CommoditySellPanel
	{
		private CommoditySellPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<CommoditySellPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<CommoditySellPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_SelectedCount.SubscribeToTextMeshPro(m_SellNumber).AddTo(this);
			m_SelectedCount.Select(val => val * m_MarketCommodityMoel.commodityPrice.Value).SubscribeToTextMeshPro(m_TotalValue).AddTo(this);
			m_SelectedCount.Where(val => val > 0).Subscribe(val => { HandleMaxAndMinBtnActive(val); }).AddTo(this);
			m_MarketCommodityMoel.commodityPrice.SubscribeToTextMeshPro(m_UnitPriceValue).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			m_SellBtn.OnClickAsObservable().Subscribe(_ => { HandleSellEvt(); }).AddTo(this);
		}
	}
}
