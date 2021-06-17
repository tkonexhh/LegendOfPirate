using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class MainMenuPanelData : UIPanelData
	{
		public MainMenuPanelData()
		{
		}
	}
	
	public partial class MainMenuPanel
	{
		private MainMenuPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<MainMenuPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<MainMenuPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}


        private void OnClickAddListener()
        {
			RoleBtn.onClick.AddListener(() =>
			{
				UIMgr.S.OpenPanel(UIID.RoleGroupPanel);
			});

        }

    }
}
