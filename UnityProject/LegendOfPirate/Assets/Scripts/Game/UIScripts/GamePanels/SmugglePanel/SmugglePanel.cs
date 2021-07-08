using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class SmugglePanel : AbstractAnimPanel
	{
		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
			AllocatePanelData();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			InitData();

			InitPanelMsg();
			
			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();
		}

        private void InitData()
        {

        }

        private void InitPanelMsg()
        {
			m_ScrollView.SetCellRenderer(OnOrderChange);
			m_ScrollView.SetDataCount(m_PanelData.smuggleModel.orderModelList.Count);
        }

        private void OnOrderChange(Transform root, int index)
        {
           
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
		#endregion

		#region Button Event
		private void ExitBtnEvent()
		{
			HideSelfWithAnim();
		}
		#endregion
	}
}
