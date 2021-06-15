using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class CongratulationPanelData : UIPanelData
	{
		public CongratulationPanelData()
		{
		}
	}
	
	public partial class CongratulationPanel
	{
		private CongratulationPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<CongratulationPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<CongratulationPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		private void OnClickAddListener()
		{
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                BgBtnEvent();
            });
        }
	}
}
