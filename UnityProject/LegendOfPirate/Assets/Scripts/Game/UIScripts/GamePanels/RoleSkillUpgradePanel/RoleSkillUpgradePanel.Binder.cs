using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleSkillUpgradePanelData : UIPanelData
	{
		public RoleSkillUpgradePanelData()
		{
		}
	}
	
	public partial class RoleSkillUpgradePanel
	{
		private RoleSkillUpgradePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<RoleSkillUpgradePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleSkillUpgradePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_RoleSkillModel?.skillLevel.SubscribeToTextMeshPro(m_RoleSkillTitle);
		}
		
		private void BindUIToModel()
		{
		}
	}
}
