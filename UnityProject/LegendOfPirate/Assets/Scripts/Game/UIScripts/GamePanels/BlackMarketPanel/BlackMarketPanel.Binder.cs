using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class BlackMarketPanelData : UIPanelData
	{
		public BlackMarketPanelData()
		{
		}
	}
	
	public partial class BlackMarketPanel
	{
		private BlackMarketPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<BlackMarketPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<BlackMarketPanelData>.S.Recycle(m_PanelData);
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
