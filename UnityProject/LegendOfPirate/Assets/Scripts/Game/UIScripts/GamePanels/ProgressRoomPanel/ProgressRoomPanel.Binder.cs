using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class ProgressRoomPanelData : UIPanelData
	{
		public ProgressRoomPanelData()
		{
		}
	}
	
	public partial class ProgressRoomPanel
	{
		private ProgressRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<ProgressRoomPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<ProgressRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
