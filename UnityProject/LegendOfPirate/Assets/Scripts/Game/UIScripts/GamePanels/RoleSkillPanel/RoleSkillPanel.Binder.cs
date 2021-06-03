using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleSkillPanelData : UIPanelData
	{
		public RoleSkillPanelData()
		{
		}
	}
	
	public partial class RoleSkillPanel
	{
		private RoleSkillPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleSkillPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleSkillPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
