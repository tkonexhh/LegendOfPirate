using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class SmugglePanelData : UIPanelData
	{
		public SmuggleModel smuggleModel;
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
		 	m_PanelData.smuggleModel = ModelMgr.S.GetModel<SmuggleModel>();
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
			m_ExitBtn.OnClickAsObservable().Subscribe(_ =>
            {
				ExitBtnEvent();
            });
        }
	}
}
