using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;

namespace GameWish.Game
{
	public class RoleGrowthPanelData : UIPanelData
	{
		public int roleID;
		public RoleModel roleModel = null;
		public RoleGrowthPanelData()
		{
		}
	}
	
	public partial class RoleGrowthPanel
	{
		private RoleGrowthPanelData m_PanelData = null;
	    private int   exHp;
        private float exAtk;
        private int   exExp;
        private int   starLevel;

        private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleGrowthPanelData>();
			
		}

		private void GetRoleData(int roleId) 
		{
			m_PanelData.roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(roleId);
			exHp = m_PanelData.roleModel.curHp.Value;
			exAtk = m_PanelData.roleModel.curHp.Value;
			exExp = m_PanelData.roleModel.curExp.Value;
			starLevel = m_PanelData.roleModel.starLevel.Value;
			m_PanelData.roleModel.level.Value += 1;
			SetPanelMsg();
		}

		private void SetPanelMsg() 
		{
			var GrowthMsg = m_Content.GetComponentsInChildren<TextMeshProUGUI>();
			GrowthMsg[0].text = string.Format("Hp:{0}¡ú{1}", exHp, m_PanelData.roleModel.curHp.Value);
			GrowthMsg[1].text = string.Format("Attack:{0}¡ú{1}", exAtk, m_PanelData.roleModel.curAtk.Value);
			GrowthMsg[2].text = string.Format("Exp:{0}¡ú{1}", exExp, m_PanelData.roleModel.curExp.Value);
		}

		private void ReleasePanelData()
		{
			ObjectPool<RoleGrowthPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{

		}
		
	}
}
