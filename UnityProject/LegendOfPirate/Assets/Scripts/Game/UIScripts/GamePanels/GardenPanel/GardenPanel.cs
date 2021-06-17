using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

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
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			OpenDependPanel(EngineUI.MaskPanel, -1);
			AllocatePanelData(args);
			
			BindModelToUI();
			BindUIToModel();
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
