using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class TestPanelData : UIPanelData
	{
		public TestPanelData()
		{
		}
	}
	
	public partial class TestPanel
	{
		private TestPanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<TestPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<TestPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
