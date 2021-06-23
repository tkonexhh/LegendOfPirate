using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class WareHousePanelData : UIPanelData
	{
		public InventoryModel inventoryModel;
		public WareHousePanelData()
		{
		}
	}
	
	public partial class WareHousePanel
	{
		private WareHousePanelData m_PanelData = null;
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<WareHousePanelData>();
			m_PanelData.inventoryModel = ModelMgr.S.GetModel<InventoryModel>();
		}

		private void ReleasePanelData()
		{
			ObjectPool<WareHousePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			foreach (var item in m_PanelData.inventoryModel.InventoryItemDics)
			{
				item.Value.ObserveCountChanged()
						  .Subscribe(val => { OnRefreshItemListByType(openItemType); })
					  	  .AddTo(this);
			}
		
		}
		private void BindUIToModel() { }
	}
}
