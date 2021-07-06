using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleGrowthPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			
			AllocatePanelData();

			InitializePassValue(args);

			if (args != null && args.Length > 0)
				GetRoleData((int)args[0]);

			BindModelToUI();
			BindUIToModel();
			AddPanelListener();
		}

        private void AddPanelListener()
        {
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); }).AddTo(this);
        }

        private void InitializePassValue(object[] args)
		{
			m_PanelData.roleID = (int)args[0];
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
