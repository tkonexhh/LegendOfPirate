using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class ChargingInterfacePanelData : UIPanelData
	{
		public InternalPurchaseModel internalPurchaseModel;

		public ChargingInterfacePanelData()
		{
		}
	}
	
	public partial class ChargingInterfacePanel
	{
		private ChargingInterfacePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<ChargingInterfacePanelData>();

			m_PanelData.internalPurchaseModel = ModelMgr.S.GetModel<InternalPurchaseModel>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<ChargingInterfacePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
	}
}
