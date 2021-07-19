using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using System;

namespace GameWish.Game
{
	public partial class PubPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();

			OnClickAsObservable();

			AllocatePanelData();
			
			BindModelToUI();
			
			BindUIToModel();
		}

        protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
		}
		
		protected override void BeforDestroy()
		{
			base.BeforDestroy();
			
			ReleasePanelData();
		}

		#region OnClickAsObservable
		private void OnClickAsObservable()
		{
			m_ExitBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
		}

		private void RecruitBtn()
		{
			PubConfig pubConfig = m_PanelData.pubModel.SingleDraw();
			if (pubConfig.id != 0)
			{
				Log.e("1 = " + pubConfig.id);
			}
		}

		private void TenRecruitBtn()
		{
			List<PubConfig> list = m_PanelData.pubModel.ContinuousPumping();
			if (list != null)
			{
				foreach (var item in list)
				{
					Log.e("10 = " + item.id);
				}
			}
		}
		#endregion
	}
}
