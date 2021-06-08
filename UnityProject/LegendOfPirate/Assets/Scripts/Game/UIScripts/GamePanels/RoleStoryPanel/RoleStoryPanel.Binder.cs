using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RoleStoryPanelData : UIPanelData
	{
		
		public RoleStoryPanelData()
		{
		}
	}
	
	public partial class RoleStoryPanel
	{
		private RoleStoryPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleStoryPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleStoryPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
