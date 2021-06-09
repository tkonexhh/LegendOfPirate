using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RandomDefenseChooseRolePanelData : UIPanelData
	{
		public RandomDefenseChooseRolePanelData()
		{
		}
	}
	
	public partial class RandomDefenseChooseRolePanel
	{
		private RandomDefenseChooseRolePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<RandomDefenseChooseRolePanelData>();
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
		
	}
}
