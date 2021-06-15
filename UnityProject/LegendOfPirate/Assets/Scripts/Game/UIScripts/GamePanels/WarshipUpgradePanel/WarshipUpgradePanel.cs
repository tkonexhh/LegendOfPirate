using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class WarshipUpgradePanel : AbstractAnimPanel
	{
		private ScheduleNode m_CountdownActionNode;

		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			RegisterEvents();

			AllocatePanelData(args);
			
			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();

			InitData();

		}
		private void InitData()
		{
			int countDown = 100;
			//暂时写着，后移入对应的Mgr
			m_CountdownActionNode = ActionNode.Allocate<ScheduleNode>();

			m_CountdownActionNode.SetParams(this, System.DateTime.Now, 100, 1)
				.AddOnStartCallback((node) => { Debug.LogError("On countdown start"); })
				.AddOnTickCallback((node) => {
					UpgradeResTimeValue.text = DateFormatHelper.FormatTime(--countDown);

					Debug.LogError("On countdown tick : " + Time.time);
				})
				.AddOnEndCallback((node) => { Debug.LogError("On countdown end"); ActionNode.Recycle2Cache(m_CountdownActionNode); })
				.Execute();

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
		#region Button Event
		private void BgBtnEvent()
		{
			HideSelfWithAnim();
		}
        #endregion

    }
}
