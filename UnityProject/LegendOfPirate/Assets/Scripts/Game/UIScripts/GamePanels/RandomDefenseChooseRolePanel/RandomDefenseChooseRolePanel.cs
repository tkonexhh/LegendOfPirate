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
	public class RandomDefenseChooseRoleModule
	{
		public IntReactiveProperty index;
		public BoolReactiveProperty isSelected;
		public RoleModel roleModel;

		public RandomDefenseChooseRoleModule(int index, bool selected, RoleModel reactiveProperty)
		{
			this.index = new IntReactiveProperty(index);
			this.isSelected = new BoolReactiveProperty(selected);
			this.roleModel = reactiveProperty;
		}

		#region Public
		public int GetRoleID()
		{
			return roleModel.id;
		}
        #endregion
    }
    public class TopSelectedRoleModule
	{
		public ReactiveProperty<RandomDefenseChooseRoleModule> randomDefenseChooseRoleModule = new ReactiveProperty<RandomDefenseChooseRoleModule>();
		public IReadOnlyReactiveProperty<bool> isEmpty;

		public TopSelectedRoleModule()
		{
			//new BoolReactiveProperty(randomDefenseChooseRoleModule == null ? true : false);
			this.isEmpty = randomDefenseChooseRoleModule.Select(x => x == null ? true : false).ToReactiveProperty();
		}
	}
	#endregion
	public partial class RandomDefenseChooseRolePanel : AbstractAnimPanel
	{
		[Header("额外")]
		[SerializeField]
		private UGridListView m_RandomDefenseChooseRoleUGridList;	
		[SerializeField]
		private GameObject m_TopSelectedRole;

		#region Data
		private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
		private ReactiveCollection<TopSelectedRoleModule> m_TopSelectedRoleModuleModules = new ReactiveCollection<TopSelectedRoleModule>();
		private ReactiveCollection<RandomDefenseChooseRoleModule> m_RandomDefenseChooseRoleModules = new ReactiveCollection<RandomDefenseChooseRoleModule>();
		private ReactiveCollection<TopSelectedRole> m_TopSelectedRoles = new ReactiveCollection<TopSelectedRole>();

		private ReactiveCollection<RoleModel> m_SelectedRoles = new ReactiveCollection<RoleModel>();
		#endregion
		#region AbstractAnimPanel

		protected override void OnUIInit()
		{
			base.OnUIInit();
			m_SelectedRoles.Add(new RoleModel (new RoleData (0,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (1,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (2,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (3,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (4,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (5,true)));
			m_SelectedRoles.Add(new RoleModel (new RoleData (6,true)));
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
				case EventID.OnSelectedRole:
					HandleSelected((RandomDefenseChooseRoleModule)param[0]);
					break;
			}
		}

    
        #endregion
        #region Other Method
        private void InitData()
		{
			m_RandomDefenseChooseRoleUGridList.SetCellRenderer(OnBottomCellRenderer);

			m_RandomDefenseChooseRoleUGridList.SetDataCount(m_SelectedRoles.Count);

			CreateTopSelectedRole();
		}

        private void OnBottomCellRenderer(Transform root, int index)
        {
			RandomDefenseChooseRoleModule randomDefenseChooseRoleModule = m_RandomDefenseChooseRoleModules.FirstOrDefault(item => item.index.Value == index);
			if (randomDefenseChooseRoleModule != null)
			{
				root.GetComponent<MiddleSelectRole>().OnInit(randomDefenseChooseRoleModule, m_SelectedCount);
			}
			else
			{
				RandomDefenseChooseRoleModule newRandomDefenseChooseRoleModule = new RandomDefenseChooseRoleModule(index, false, m_SelectedRoles[index]);
				m_RandomDefenseChooseRoleModules.Add(newRandomDefenseChooseRoleModule);
				root.GetComponent<MiddleSelectRole>().OnInit(newRandomDefenseChooseRoleModule, m_SelectedCount);
			}
		}

        private void CreateTopSelectedRole()
        {
            for (int i = 0; i < 3; i++)
            {
				TopSelectedRole topSelectedRole = Instantiate(m_TopSelectedRole, SelectedRoleRegion).GetComponent<TopSelectedRole>();
				topSelectedRole.OnInit();
				m_TopSelectedRoles.Add(topSelectedRole);
			}
		}
		private void HandleSelected(RandomDefenseChooseRoleModule randomDefenseChooseRoleModule)
		{
			TopSelectedRole topSelectedRole = m_TopSelectedRoles.FirstOrDefault(x => x.topSelectedRoleModule.isEmpty.Value);
            if (topSelectedRole!=null)
            {
				topSelectedRole.OnRefreshTopSelectedRoleModule(randomDefenseChooseRoleModule);
			}
            else
            {
				Debug.LogError("没有空位");
            }
        }
		#endregion
	}
}
