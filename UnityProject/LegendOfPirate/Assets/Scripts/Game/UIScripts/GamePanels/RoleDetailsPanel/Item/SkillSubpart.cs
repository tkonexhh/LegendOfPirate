using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
	public class SkillSubpart : MonoBehaviour
	{
		#region SerializeField
		[SerializeField] private Button m_RoleSkillBtn;
		[SerializeField] private TextMeshProUGUI m_RoleSkillLevel;
		[SerializeField] private Image m_RoleSkillIcon;
		[SerializeField] private Image m_RoleSkillLock;
		#endregion

		#region Data
		private RoleSkillModel m_RoleSkillModel;
		private int m_RoleID;
		#endregion

		#region Public
		public void OnInit(int roleID, RoleSkillModel roleSkillModel)
		{
			OnReset();

			m_RoleID = roleID;
			m_RoleSkillModel = roleSkillModel;

			BindModelToUI();
			OnClickAddListener();

			OnRefresh();
		}
		#endregion

		#region Private
		private void OnReset()
		{
			m_RoleSkillIcon.enabled = false;
			m_RoleSkillLock.enabled = false;
		}
		private void OnClickAddListener()
		{
			m_RoleSkillBtn.OnClickAsObservable().Subscribe(_ => { OpenRoleSkillPanel(); }).AddTo(this);
		}

		private void OpenRoleSkillPanel()
		{
			UIMgr.S.OpenTopPanel(UIID.RoleSkillUpgradePanel, PanelCallbakc, m_RoleID, m_RoleSkillModel);
		}
		private void PanelCallbakc(AbstractPanel obj)
		{
		}
		private void BindModelToUI()
		{
			m_RoleSkillModel.skillLevel
				.Select(val => HandleRoleSkillLevel(val))
				.SubscribeToTextMeshPro(m_RoleSkillLevel).AddTo(this);
		}

		private void OnRefresh()
		{
			//m_RoleSkillIcon.sprite = SpriteHandler.S.GetSprite();
		}
		private string HandleRoleSkillLevel(int val)
		{
			if (val == 0)
				val++;
			return CommonMethod.GetStringForTableKey(LanguageKeyDefine.FIXED_TITLE_LV_¢ñ) + val;
		}
		#endregion
	}
}