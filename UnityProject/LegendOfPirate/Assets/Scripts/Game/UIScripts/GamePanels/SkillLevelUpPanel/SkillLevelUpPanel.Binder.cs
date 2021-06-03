using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class SkillLevelUpPanelData : UIPanelData
	{
		public SkillLevelUpPanelData()
		{
		}
	}
	
	public partial class SkillLevelUpPanel
	{
		private SkillLevelUpPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<SkillLevelUpPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<SkillLevelUpPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
