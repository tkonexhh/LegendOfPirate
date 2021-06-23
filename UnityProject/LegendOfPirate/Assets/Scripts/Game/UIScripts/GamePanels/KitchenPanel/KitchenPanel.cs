using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class KitchenPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();

			InitPanelBtn();
        }
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			OpenDependPanel(EngineUI.MaskPanel ,- 1);

			InitUI();
		}

        private void InitUI()
        {
			var slotListView = m_KitchenSlotList.GetComponent<USimpleListView>();
			slotListView.SetCellRenderer(OnKitchenSlotCellRenderer);
			slotListView.SetDataCount(m_PanelData.GetKitchenSlotCount());

			slotListView = m_FoodSlotList.GetComponent<USimpleListView>();
			slotListView.SetCellRenderer(OnFoodSlotCellRenderer);
			slotListView.SetDataCount(m_PanelData.GetFoodSlotCount());
        }

        private void OnFoodSlotCellRenderer(Transform root, int index)
        {
			var slotItem = root.GetComponent<FoodSlot>();
			slotItem.SetSlot(m_FoodSlotToggleGroup, index, m_ElementList.GetComponent<ResElementLst>());
        }

        private void OnKitchenSlotCellRenderer(Transform root, int index)
        {
			var slotItem = root.GetComponent<KitchenSlot>();
            slotItem.SetSlot(index);
        }

        protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			CloseDependPanel(EngineUI.MaskPanel);
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}

	}
}
