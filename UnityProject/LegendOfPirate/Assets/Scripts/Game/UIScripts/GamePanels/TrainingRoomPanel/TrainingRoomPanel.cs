using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;

namespace GameWish.Game
{
	#region Other Data Class
	public class BottomTrainingRoleModule
	{
		public IntReactiveProperty index;
		public BoolReactiveProperty isSelected;

		public BottomTrainingRoleModule(int index,bool selected)
		{
			this.index = new IntReactiveProperty(index);
			this.isSelected = new BoolReactiveProperty(selected);
		}
	}
	#endregion
	public partial class TrainingRoomPanel : AbstractAnimPanel
	{
		[SerializeField]
		private UGridListView m_MiddleTrainingRoleUGridList;
		[SerializeField]
		private USimpleListView m_BottomTrainingRoleUList;

		#region Data
		private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
		private ReactiveCollection<BottomTrainingRoleModule> bottomTrainingRoleDatas = new ReactiveCollection<BottomTrainingRoleModule>();
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			RegisterEvents();

			OpenDependPanel(EngineUI.MaskPanel, -1, null);

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

			CloseDependPanel(EngineUI.MaskPanel);
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
			UnregisterEvents();
		}
		#endregion

		#region ButtonEvent
		public void TrainingUpgradeBtnEvent()
		{

		}
		public void TrainBtnEvent()
		{

		}
		public void AutoTrainBtnEvent()
		{

		}	
		public void BgBtnEvent()
		{
			HideSelfWithAnim();
		}
        #endregion

        #region EventSystem
        private void HandlerEvent(int key, object[] param)
		{
            switch ((EventID)key)
            {
                case EventID.OnBottomTrainingRole:
                    break;
            }
        }
        #endregion
        private void InitData()
		{
			BindUniRxUI();

			//TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

			m_MiddleTrainingRoleUGridList.SetCellRenderer(OnMiddleCellRenderer);
			m_BottomTrainingRoleUList.SetCellRenderer(OnBottomCellRenderer);

			m_MiddleTrainingRoleUGridList.SetDataCount(10);
			m_BottomTrainingRoleUList.SetDataCount(10);
		}
		private void OnBottomCellRenderer(Transform root, int index)
		{
			BottomTrainingRoleModule bottomTrainingRoleData = bottomTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (bottomTrainingRoleData!=null)
            {
				root.GetComponent<BottomTrainingRole>().OnInit(bottomTrainingRoleData, m_SelectedCount);
			}
            else
            {
				BottomTrainingRoleModule newBottomTrainingRoleModule = new BottomTrainingRoleModule(index, false);
				bottomTrainingRoleDatas.Add(newBottomTrainingRoleModule);
				root.GetComponent<BottomTrainingRole>().OnInit(newBottomTrainingRoleModule, m_SelectedCount);
			}
		}

		private void OnMiddleCellRenderer(Transform root, int index)
        {

			root.GetComponent<MiddleTrainingRole>().OnInit(index);
		}

        private void BindUniRxUI()
		{
			m_SelectedCount.Select(count => count + "/" + 10).SubscribeToTextMeshPro(RoleSelectNumberTMP);
		}

		public void CreateMiddleTrainingRole()
		{
			//MiddleTrainingRole middleTraining = Instantiate(MiddleTrainingRole, MiddleTrainingRoleTra.transform).GetComponent<MiddleTrainingRole>();

        }

	}
}
