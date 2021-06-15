using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RandomDefenseChooseRolePanelData : UIPanelData
	{
		public RoleGroupModel roleGroupModel;
		public RandomDefenseChooseRolePanelData()
		{
		}

		public ReactiveCollection<RoleModel> GetRoleUnlockedItemList()
		{
			return roleGroupModel.roleUnlockedItemList;
		}
			
	}
	
	public partial class RandomDefenseChooseRolePanel
	{
		private RandomDefenseChooseRolePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<RandomDefenseChooseRolePanelData>();
            try
            {
				m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
			}
            catch (System.Exception e)
            {
				Debug.LogError("e = " + e);
			}
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RandomDefenseChooseRolePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		private void OnClickAddListener()
		{
			BgBtn.OnClickAsObservable().Subscribe(_ =>
			{
				BgBtnEvent();
			});
		}
		private void RegisterEvents()
		{
			EventSystem.S.Register(EventID.OnSelectedRole, HandlerEvent);
		}
		private void UnregisterEvents()
		{
			EventSystem.S.UnRegister(EventID.OnSelectedRole, HandlerEvent);
		}
	}
}
