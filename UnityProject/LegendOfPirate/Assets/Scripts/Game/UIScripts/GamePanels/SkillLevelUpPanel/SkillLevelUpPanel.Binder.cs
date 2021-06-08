using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class SkillLevelUpPanelData : UIPanelData
	{
		public RoleSkillModel roleSkillModel { get; private set; }

		public SkillLevelUpPanelData()
		{
			roleSkillModel = ModelMgr.S.GetModel<RoleModel>().GetSkillModel(1);
		}
		public void SetRoleSkillModel(int skillid)
		{
			roleSkillModel = ModelMgr.S.GetModel<RoleModel>().GetSkillModel(skillid);
		}
		
	}
	
	public partial class SkillLevelUpPanel
	{
		private SkillLevelUpPanelData m_PanelData = null;
		public int skillId { get; set; }
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<SkillLevelUpPanelData>();
			 m_PanelData.SetRoleSkillModel(skillId);
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<SkillLevelUpPanelData>.S.Recycle(m_PanelData);
		}
        private void InitPanelWithParams(params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                skillId = (int)args[0];
            }
        }
        private void BindModelToUI()
		{
			m_PanelData.roleSkillModel.skillLevel.Subscribe(level => SkillLevel.text = "Lv." + level) ;
		
		}

       
        private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim());
		}
		
		
	}
}
