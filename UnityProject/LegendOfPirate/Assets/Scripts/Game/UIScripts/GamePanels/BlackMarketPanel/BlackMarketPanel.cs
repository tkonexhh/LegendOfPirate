using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;

namespace GameWish.Game
{
	#region Other Data Class
	public class BlackMarketCommodityModule
	{
		public IntReactiveProperty index;
		public BoolReactiveProperty isSelected;

		public BlackMarketCommodityModule(int index, bool selected)
		{
			this.index = new IntReactiveProperty(index);
			this.isSelected = new BoolReactiveProperty(selected);
		}
	}
	#endregion
	public partial class BlackMarketPanel : AbstractAnimPanel
	{
		[SerializeField]
		private UGridListView m_BlackMarketPanelUGridList;
		#region Data
		private ReactiveCollection<BlackMarketCommodityModule> blackMarketCommodityModules = new ReactiveCollection<BlackMarketCommodityModule>();
		#endregion
		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			
			AllocatePanelData(args);
			
			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();

			InitData();
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}
		#endregion
		#region ButtonEvent

		public void ExitBtnEvent()
		{
			HideSelfWithAnim();	
		}
		#endregion
		#region Other Method
		private void InitData()
		{
			m_BlackMarketPanelUGridList.SetCellRenderer(OnCellRenderer);

			m_BlackMarketPanelUGridList.SetDataCount(10);
		}

        private void OnCellRenderer(Transform root, int index)
        {
			BlackMarketCommodityModule blackMarketCommodityModule = blackMarketCommodityModules.FirstOrDefault(item => item.index.Value == index);
			if (blackMarketCommodityModule != null)
			{
				root.GetComponent<BlackMarketCommodity>().OnInit(blackMarketCommodityModule);
			}
			else
			{
				BlackMarketCommodityModule newBlackMarketCommodityModule = new BlackMarketCommodityModule(index, false);
				blackMarketCommodityModules.Add(newBlackMarketCommodityModule);
				root.GetComponent<BlackMarketCommodity>().OnInit(newBlackMarketCommodityModule);
			}
		}
        #endregion
    }
}
