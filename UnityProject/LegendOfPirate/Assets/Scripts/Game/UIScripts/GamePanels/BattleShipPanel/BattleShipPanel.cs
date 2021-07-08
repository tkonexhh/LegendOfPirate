using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class BattleShipPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
			
			AllocatePanelData();

			BindModelToUI();
			
			BindUIToModel();
			
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			InitPanelMsg();

			InitEventListener();
		}

        private void InitEventListener()
        {
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }

        private void InitPanelMsg()
        {
			m_ScrollView.SetCellRenderer(OnShipViewChange);
			m_ScrollView.SetDataCount(m_PanelData.battleshipFleetModel.battleShipModels.Count);
		}

        private void OnShipViewChange(Transform root, int index)
        {
			root.GetComponent<BattleShipSlot>().InitSlot(index);
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

	}
}
