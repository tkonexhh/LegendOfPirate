using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class TrainingRoomPanelData : UIPanelData
	{
		public TrainingRoomPanelData()
		{
		}
	}
	
	public partial class TrainingRoomPanel
	{
		private TrainingRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<TrainingRoomPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<TrainingRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
