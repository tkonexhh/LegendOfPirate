using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

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
            #region Vip
            //待优化，可以考虑弄一个注册列表
            m_PanelData.internalPurchaseModel.vipState.SubscribeToPositiveActive(m_VipIcon).AddTo(this);
			m_PanelData.internalPurchaseModel.vipState.SubscribeToPositiveActive(m_VipDiamondsNumber).AddTo(this);
			m_PanelData.internalPurchaseModel.vipState.SubscribeToPositiveActive(m_PurchasedState).AddTo(this);
			m_PanelData.internalPurchaseModel.vipState.SubscribeToPositiveActive(m_ObtainBtn).AddTo(this);
			m_PanelData.internalPurchaseModel.vipState.SubscribeToNegativeActive(m_NotPurchasedState).AddTo(this);
			m_PanelData.internalPurchaseModel.vipState.SubscribeToNegativeActive(m_PurchaseBtn).AddTo(this);

			m_PanelData.internalPurchaseModel.receiveToday.SubscribeToNegativeInteractable(m_ObtainBtn);
			m_PanelData.internalPurchaseModel.receiveToday.SubscribeToPositiveActive(m_VipDiamondsCountDown);

			m_PanelData.internalPurchaseModel.purchaseProfit.Select(val=> MachiningPurchaseProfit(val)).SubscribeToTextMeshPro(m_ProfitValue).AddTo(this);
		
			m_PanelData.internalPurchaseModel.purchasePrice.Select(val => MachiningPurchasePrice(val)).SubscribeToTextMeshPro(m_VipDetailsRatio).AddTo(this);
		
			m_PanelData.internalPurchaseModel.vipDueDate.Select(val => MachiningVipDueDate(val)).SubscribeToTextMeshPro(m_DueDate).AddTo(this);

			m_PanelData.internalPurchaseModel.totalDiamondsNumber.SubscribeToTextMeshPro(m_TotalDiamondsNumber).AddTo(this);

			m_PanelData.internalPurchaseModel.todayDiamondsNumber.SubscribeToTextMeshPro(m_TodayDiamondsNumber).AddTo(this);

			m_PanelData.internalPurchaseModel.dailyDiamondsNumberNotFirst.SubscribeToTextMeshPro(m_EveryDiamondsNumber).AddTo(this);
		
			m_PanelData.internalPurchaseModel.dailyDiamondsNumberInFirst.SubscribeToTextMeshPro(m_VipDiamondsNumber).AddTo(this);

			m_PanelData.internalPurchaseModel.deceivedDiamondsNumber.SubscribeToTextMeshPro(m_QuantityReceivedNumber).AddTo(this);

			m_PanelData.internalPurchaseModel.refreshCountdown.SubscribeToTextMeshPro(m_VipDiamondsCountDown).AddTo(this);
			#endregion
		}

		private void BindUIToModel()
		{
			m_PurchaseBtn.OnClickAsObservable().Subscribe(_ => { HandlePurchaseBtn(); }).AddTo(this);
			m_ObtainBtn.OnClickAsObservable().Subscribe(_ => { HandleObtainBtn(); }).AddTo(this);
		}
	}
}
