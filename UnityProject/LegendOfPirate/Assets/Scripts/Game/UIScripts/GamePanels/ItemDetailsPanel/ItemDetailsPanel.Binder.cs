using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class ItemDetailsPanelData : UIPanelData
	{
		public ItemDetailsPanelData()
		{
		}
	}
	
	public partial class ItemDetailsPanel
	{
		private ItemDetailsPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<ItemDetailsPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<ItemDetailsPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
