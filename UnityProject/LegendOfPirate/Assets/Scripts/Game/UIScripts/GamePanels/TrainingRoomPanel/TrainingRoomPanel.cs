using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class TrainingRoomPanel : AbstractAnimPanel
	{
		[SerializeField]
		private UGridListView m_MiddleTrainingRoleUGridList;
		[SerializeField]
		private USimpleListView m_BottomTrainingRoleUList;
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

		private void InitData()
		{
			InitFixedText();

			//TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

			m_MiddleTrainingRoleUGridList.SetCellRenderer(OnMiddleCellRenderer);
			m_BottomTrainingRoleUList.SetCellRenderer(OnBottomCellRenderer);

			m_MiddleTrainingRoleUGridList.SetDataCount(10);
			m_BottomTrainingRoleUList.SetDataCount(10);
		}
		private void OnBottomCellRenderer(Transform root, int index)
		{
			Debug.LogError("Index = " + index);
			root.GetComponent<BottomTrainingRole>().OnInit(index);
		}

		private void OnMiddleCellRenderer(Transform root, int index)
        {
			Debug.LogError("Index = " + index);
			root.GetComponent<MiddleTrainingRole>().OnInit(index);
		}

        private void InitFixedText()
		{

		}

		public void CreateMiddleTrainingRole()
		{
			//MiddleTrainingRole middleTraining = Instantiate(MiddleTrainingRole, MiddleTrainingRoleTra.transform).GetComponent<MiddleTrainingRole>();

        }

	}
}
