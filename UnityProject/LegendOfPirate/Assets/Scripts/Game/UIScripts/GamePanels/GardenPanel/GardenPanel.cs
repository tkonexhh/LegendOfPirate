using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public enum GardenState
    {
        /// <summary>
        /// ������
        /// </summary>
        Free = 0,
        /// <summary>
        /// ��ֲ��
        /// </summary>
        Plant= 1,
		///<summary>
		///ѡ��
		///</summary>
		Select=2,
		///<summary>
		///����
		///</summary>
		WaitingHarvest=3
	}
    public partial class GardenPanel : AbstractAnimPanel
	{
		[SerializeField] private USimpleListView m_PlantSlotlstView;
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

		private void InitUI() 
		{
			m_PlantSlotlstView.SetCellRenderer(OnPlantSlotCellRenderer);
			m_PlantSlotlstView.SetDataCount(m_PanelData.GetSlotCount());
		}

        private void OnPlantSlotCellRenderer(Transform root, int index)
        {
			var slotItem = root.GetComponent<GardenPlantSlot>();
			slotItem.SetInit(m_Content,index);
        }
		
	}
}
