using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class SkillLevelUpPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim());
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			InitPanelWithParams(args);
			AllocatePanelData();
			
			BindModelToUI();
			BindUIToModel();

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
		
	}
}
