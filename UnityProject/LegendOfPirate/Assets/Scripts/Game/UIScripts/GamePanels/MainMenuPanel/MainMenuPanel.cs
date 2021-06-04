using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class MainMenuPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			
			AllocatePanelData();
			
			BindModelToUI();
			BindUIToModel();

			UIMgr.S.OpenPanel(UIID.WorldUIPanel);

			RoleDetailsBtn.OnClickAsObservable().Subscribe(_=> {
                //Transform tran = GameObject.FindGameObjectWithTag("Finish").transform;
                //FloatMeshMessage.S.ShowMsg("##", tran);
                //WorldUIPanel.S.ShowCriticalInjuryText(tran, "+1",true);

                //UIMgr.S.OpenPanel(UIID.RoleEquipDetailsPanel);
                //UIMgr.S.OpenPanel(UIID.EvolutionSolePanel);
                //UIMgr.S.OpenPanel(UIID.RoleSkillPanel);
                //UIMgr.S.OpenPanel(UIID.RoleStoryPanel);
            });
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}
		
	}
}
