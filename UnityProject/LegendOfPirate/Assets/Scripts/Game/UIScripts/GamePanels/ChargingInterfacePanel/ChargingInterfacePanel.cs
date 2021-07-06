using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class ChargingInterfacePanel : AbstractAnimPanel
	{
		#region SerializeField
		[SerializeField] private GameObject m_DailySelectionItem;
		#endregion

		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
			
			AllocatePanelData();
			
			BindModelToUI();
			
			BindUIToModel();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
		}
		
		protected override void BeforDestroy()
		{
			base.BeforDestroy();
			
			ReleasePanelData();
		}
		#endregion

		#region OnClickAsObservable
		private void HandlePurchaseBtn()
		{
			m_PanelData.internalPurchaseModel.PurchaseVip();
		}

        private void HandleObtainBtn()
		{
			m_PanelData.internalPurchaseModel.CollectDiamonds();
		}
		#endregion

		#region Private
		private string MachiningPurchaseProfit(int profit)
		{
			return string.Format(LanguageKeyDefine.INTERNALPURCHASE_PROFITTITLE,profit);
		} 

		private string MachiningPurchasePrice(float price)
		{
			return string.Format(LanguageKeyDefine.INTERNALPURCHASE_PRICE, price);
		}	

		private string MachiningVipDueDate(DateTime vueDate)
		{
			return string.Format(LanguageKeyDefine.INTERNALPURCHASE_DUEDATE, CommonMethod.GetFormatDate(vueDate));
		}
		
		#endregion
	}
}
