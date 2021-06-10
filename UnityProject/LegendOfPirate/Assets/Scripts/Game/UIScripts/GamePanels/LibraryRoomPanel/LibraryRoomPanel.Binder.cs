using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class LibraryRoomPanelData : UIPanelData
	{
		public LibraryRoomPanelData()
		{
		}
	}
	
	public partial class LibraryRoomPanel
	{
		private LibraryRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<LibraryRoomPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<LibraryRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}

        private void BindUIToModel()
		{
		}
		private void RegisterEvents()
		{

		}

		private void OnClickAddListener()
		{
			//TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ => {
			//	Debug.LogError("Upgrade");//TODO
			//});
			
		}
		private void UnregisterEvents()
		{

		}
	}
}
