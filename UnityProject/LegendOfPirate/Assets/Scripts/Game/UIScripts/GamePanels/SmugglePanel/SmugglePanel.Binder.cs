using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class SmugglePanelData : UIPanelData
	{
		public SmugglePanelData()
		{
		}
	}
	
	public partial class SmugglePanel
	{
		private SmugglePanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<SmugglePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<SmugglePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}

		private void OnClickAddListener()
		{
			ExitBtn.OnClickAsObservable().Subscribe(_ =>
            {
				ExitBtnEvent();
            });
        }
	}
}
