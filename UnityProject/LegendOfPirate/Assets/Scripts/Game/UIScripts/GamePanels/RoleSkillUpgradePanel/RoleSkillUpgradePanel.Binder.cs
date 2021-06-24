using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public class RoleSkillUpgradePanelData : UIPanelData
	{
		public RoleSkillUpgradePanelData()
		{
		}
	}
	
	public partial class RoleSkillUpgradePanel
	{
		private RoleSkillUpgradePanelData m_PanelData = null;
		
		private void AllocatePanelData()
		{
			m_PanelData = UIPanelData.Allocate<RoleSkillUpgradePanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<RoleSkillUpgradePanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
        {
            m_RoleSkillModel?.skillLevel
				.Select(val=> HandleRoleSkillLevel(val))
				.SubscribeToTextMeshPro(m_RoleSkillLevel).AddTo(this);

			m_RoleSkillModel?.skillLevel
				.Select(val=> HandleSkillDifferentStates(val))
				.SubscribeToTextMeshPro(m_NextLevelExplain).AddTo(this);

			m_RoleSkillModel?.skillLevel
				.Select(val => HandleSkillUpgradeClip())
				.SubscribeToTextMeshPro(m_SkillUpgradeMaterialsnumber).AddTo(this);

			m_RoleSkillModel?.skillLevel
			.Select(val => HandleUpgradeBtnTMP(val))
			.SubscribeToTextMeshPro(m_SkillUpgradeText).AddTo(this);
		}
		private void BindUIToModel()
		{
			m_SkillUpgradeBtn.OnClickAsObservable().Subscribe(_ => { HandleSkillUpgradeBtn(); }).AddTo(this);
		}
	}
}
