using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleSkillUpgradeSuccessPanel : AbstractAnimPanel
	{
		#region Data
		private RoleSkillModel m_RoleSkillModel;
		private RoleModel m_RoleModel;
		#endregion

		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
			
			AllocatePanelData();

			OnClickAddListener();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			//OpenDependPanel(EngineUI.MaskPanel,-1,null);

			HandleTransmitValue(args);

			BindModelToUI();
			BindUIToModel();

			InitData();
		}

        protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();

			//CloseDependPanel(EngineUI.MaskPanel);
		}
		
		protected override void OnClose()
		{
			base.OnClose();
		}
		
		protected override void BeforDestroy()
		{
			base.BeforDestroy();
			
			ReleasePanelData();
		}
		#endregion

		#region OnClickAddListener
		private void OnClickAddListener()
		{
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => { HideSelfWithAnim(); }).AddTo(this);
		}
		#endregion

		#region Private
		private void HandleTransmitValue(params object[] args)
		{
			m_RoleModel = args[0] as RoleModel;
			m_RoleSkillModel = args[1] as RoleSkillModel;
		}
		private void InitData()
		{
			CommonMethod.TMPFlipUpEffect(m_GrowthMsg, m_RoleSkillModel.GetLastSkillLevel(),100000 /*m_RoleSkillModel.skillLevel.Value*/);
			//CommonMethod.TMPFlipUpEffect(m_RoleSkillValue, m_RoleSkillModel.GetLastSkillLevel(), m_RoleSkillModel.skillLevel.Value);
			//CommonMethod.TMPFlipUpEffect(m_RoleCapacity, m_RoleSkillModel.GetLastSkillLevel(), m_RoleSkillModel.skillLevel.Value);
			//m_RoleSkillImg.sprite = SpriteHandler.S.GetSprite();
			//m_RoleImg.sprite = SpriteHandler.S.GetSprite();
		}
		#endregion
	}
}
