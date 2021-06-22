using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public enum ProcessSlotState 
	{
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
		/// <summary>
		/// 加工中
		/// </summary>
		Processing = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        Selected = 3,
    }
	public partial class ProgressRoomPanel : AbstractAnimPanel
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
			OpenDependPanel(EngineUI.MaskPanel, -1);

			InitUI();
		}

		private void InitUI() 
		{
			var slotListView = m_ProcessingSlotList.GetComponent<USimpleListView>();
			slotListView.SetCellRenderer(OnProcessingSlotCellRenderer);
			slotListView.SetDataCount(m_PanelData.GetProcessingSlotCount());

			slotListView = m_PartList.GetComponent<USimpleListView>();
			slotListView.SetCellRenderer(OnPartSlotCellRenderer);
			slotListView.SetDataCount(m_PanelData.GetPartSlotCount());

		}


        private void OnPartSlotCellRenderer(Transform root, int index)
        {
            var slotItem = root.GetComponent<ProcessingPart>();
            slotItem.SetInit(m_PartToggleGroup,index,m_ElementList.GetComponent<ResElementLst>());
        }

        private void OnProcessingSlotCellRenderer(Transform root, int index)
        {
			var slotItem = root.GetComponent<ProcessingSlot>();
			slotItem.SetInit(index);
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
