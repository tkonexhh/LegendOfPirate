using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class RoleDetailsPanel : AbstractAnimPanel
	{
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
			RegisterEvents();
			OnClickAddListener();
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
			UnregisterEvents();
			
		}
		
	}
}
