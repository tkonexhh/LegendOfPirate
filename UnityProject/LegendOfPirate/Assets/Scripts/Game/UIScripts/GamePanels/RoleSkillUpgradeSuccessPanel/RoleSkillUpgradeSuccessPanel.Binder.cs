using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleSkillUpgradeSuccessPanelData : UIPanelData
	{
		public RoleSkillUpgradeSuccessPanelData()
		{
		}
	}
	
	public partial class RoleSkillUpgradeSuccessPanel
	{
		private RoleSkillUpgradeSuccessPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<RoleSkillUpgradeSuccessPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleSkillUpgradeSuccessPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
