using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class RandomDefensePanelData : UIPanelData
	{
		public RandomDefensePanelData()
		{
		}
	}
	
	public partial class RandomDefensePanel
	{
		private RandomDefensePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<RandomDefensePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RandomDefensePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		private void OnClickAddListener()
		{
            BlackExitBg.OnClickAsObservable().Subscribe(_ =>
            {
				BlackExitBgEvent();
            });
        }
	}
}
