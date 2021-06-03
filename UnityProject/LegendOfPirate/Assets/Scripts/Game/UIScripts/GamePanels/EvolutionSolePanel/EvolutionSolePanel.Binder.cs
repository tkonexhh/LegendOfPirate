using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class EvolutionSolePanelData : UIPanelData
	{
		public EvolutionSolePanelData()
		{
		}
	}
	
	public partial class EvolutionSolePanel
	{
		private EvolutionSolePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<EvolutionSolePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<EvolutionSolePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
