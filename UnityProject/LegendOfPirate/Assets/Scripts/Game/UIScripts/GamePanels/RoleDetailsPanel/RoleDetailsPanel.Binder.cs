using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleDetailsPanelData : UIPanelData
	{
		public RoleGroupModel roleGroupModel { get; private set; }
		public RoleDetailsPanelData()
		{
			roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
		}
	}
	
	public partial class RoleDetailsPanel
	{
		private RoleDetailsPanelData m_PanelData = null;
		private int m_RoleId;
		private RoleModel m_RoleModel;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleDetailsPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleDetailsPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_RoleModel.level.SubscribeToTextMeshPro(RoleLevel);
			
		}
		
		private void BindUIToModel()
		{

		}

		private void InitSelfPanelData(params object[] args)
        {
            if (args !=null && args.Length > 0)
            {
                if (args.Length>=1)
                {
					m_RoleId = (int)args[0];
                }
            }
			m_RoleModel = m_PanelData.roleGroupModel.GetRoleModel(m_RoleId);
			RoleName.text = m_RoleModel.name;
			//RegionRoleName.text
			RoleLevel.text = string.Format("LV.{0}", m_RoleModel.level);

        }


        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }

		private void OnClickAddListener()
        {
			StoryBtn.onClick.AddListener(() => { });
			LeftRoleBtn.OnClickAsObservable().Subscribe();
			RightRoleBtn.OnClickAsObservable().Subscribe();

		}

    }
}
