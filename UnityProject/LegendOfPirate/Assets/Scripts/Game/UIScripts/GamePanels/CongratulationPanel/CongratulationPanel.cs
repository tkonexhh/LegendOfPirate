using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class CongratulationPanel : AbstractAnimPanel
	{
		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			OpenDependPanel(EngineUI.MaskPanel, -1, null);
			
			AllocatePanelData(args);
			
			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();

			CloseDependPanel(EngineUI.MaskPanel);
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}
		#endregion
		#region Button Event
		private void BgBtnEvent()
		{
			HideSelfWithAnim();
		}
		#endregion
	}
}
