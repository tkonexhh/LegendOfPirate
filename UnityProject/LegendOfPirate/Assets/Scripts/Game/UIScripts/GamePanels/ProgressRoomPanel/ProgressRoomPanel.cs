using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public enum ProcessSlotState 
	{
        /// <summary>
        /// ������
        /// </summary>
        Free = 0,
		/// <summary>
		/// �ӹ���
		/// </summary>
		Processing = 1,
        /// <summary>
        /// δ����
        /// </summary>
        Locked = 2,
        /// <summary>
        /// ѡ����δ��ʼ
        /// </summary>
        Selected = 3,
    }
	public partial class ProgressRoomPanel : AbstractAnimPanel
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
