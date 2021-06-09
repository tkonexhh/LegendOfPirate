using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class AlchemyRoomPanelData : UIPanelData
	{
		public AlchemyRoomPanelData()
		{
		}
	}
	
	public partial class AlchemyRoomPanel
	{
		private AlchemyRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<AlchemyRoomPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<AlchemyRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
