using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using TMPro;
namespace GameWish.Game
{
	public partial class WarshipUpgradePanel : AbstractAnimPanel
	{
		private ScheduleNode m_CountdownActionNode;

		protected override void OnUIInit()
		{
			base.OnUIInit();

            AllocatePanelData();
        }
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			RegisterEvents();

			

			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();

			InitData(args);

			InitPanelMsg();

			AddPanelEventListener();

		}

        private void InitData(params object[] args)
		{
			//int countDown = 100;
			////暂时写着，后移入对应的Mgr
			//m_CountdownActionNode = ActionNode.Allocate<ScheduleNode>();

			//m_CountdownActionNode.SetParams(this, System.DateTime.Now, 100, 1)
			//	.AddOnStartCallback((node) => { Debug.LogError("On countdown start"); })
			//	.AddOnTickCallback((node) => {
			//		//m_UpgradeResTimeValue.text = DateFormatHelper.FormatTime(--countDown);

			//		Debug.LogError("On countdown tick : " + Time.time);
			//	})
			//	.AddOnEndCallback((node) => { Debug.LogError("On countdown end"); ActionNode.Recycle2Cache(m_CountdownActionNode); })
			//	.Execute();

			m_PanelData.battleShipModel = m_PanelData.battleShipFleetModel.GetBattleShipModel((int)args[0]);

			if (m_PanelData.shipIndex == null)
			{
				m_PanelData.shipIndex = new IntReactiveProperty(m_PanelData.battleShipFleetModel.GetBattleShipIndexById((int)args[0]));
			}
			else 
			{
				m_PanelData.shipIndex.Value = (int)args[0];
			}
		}

        private void InitPanelMsg()
        {
			m_TitileName.text = m_PanelData.battleShipModel.GetShipName();
			//TODO 设置Sprite
			m_ScrollView.SetCellRenderer(OnUpGradeResChange);
			m_ScrollView.SetDataCount(m_PanelData.battleShipModel.GetShipConfig().strengthenCost.Count);


		    
        }

        private void OnUpGradeResChange(Transform root, int index)
        {
			//TODO 设置ResSprite
			root.GetComponentInChildren<TextMeshProUGUI>().text = m_PanelData.battleShipModel.GetShipConfig().strengthenCost[index].resCount.ToString();


		}

        private void AddPanelEventListener()
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
		#region Button Event
		private void BgBtnEvent()
		{
			HideSelfWithAnim();
		}
        #endregion

    }
}
