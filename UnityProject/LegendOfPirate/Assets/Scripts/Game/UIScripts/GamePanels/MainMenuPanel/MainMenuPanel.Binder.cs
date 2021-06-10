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
		
		private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<MainMenuPanelData>();
            RoleDetailsBtn.OnClickAsObservable().Subscribe(_ =>
            {
				//Transform tran = GameObject.FindGameObjectWithTag("Finish").transform;
				//FloatMeshMessage.S.ShowMsg("##", tran);
				//WorldUIPanel.S.ShowCriticalInjuryText(tran, "+1",true);

				//UIMgr.S.OpenPanel(UIID.RoleEquipDetailsPanel);
				//UIMgr.S.OpenPanel(UIID.EvolutionSolePanel);
				//UIMgr.S.OpenPanel(UIID.RoleSkillPanel);
				//UIMgr.S.OpenPanel(UIID.RoleStoryPanel);
				//UIMgr.S.OpenPanel(UIID.RoleDetailsPanel, 1);
				UIMgr.S.OpenPanel(UIID.FishingPanel);	
            });
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
		
	}
}
