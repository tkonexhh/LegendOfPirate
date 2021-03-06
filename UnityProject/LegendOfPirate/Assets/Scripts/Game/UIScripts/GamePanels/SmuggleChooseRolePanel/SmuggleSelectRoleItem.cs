using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;
namespace GameWish.Game
{
	public class SmuggleSelectRoleItem : MonoBehaviour
	{
		[SerializeField] private Image m_RoleIcon;
		[SerializeField] private Button m_ItemButton;
		[SerializeField] private TextMeshProUGUI m_AddtionTxt;
		private int m_RoleId=-1;
		private SmuggleOrderModel m_SmuggleOrderModel;

		public void InitItem(SmuggleOrderModel smuggleOrderModel) 
		{
			m_ItemButton.OnClickAsObservable().Subscribe(_ => OnRoleSelectedItemClick()).AddTo(this);
			m_SmuggleOrderModel = smuggleOrderModel;
		}

        public void RefreshItem(int roleId) 
		{
			if (roleId <= 0)
			{
				m_ItemButton.gameObject.SetActive(false);
				return;
			} 
			m_RoleId = roleId;

			var roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(roleId);
			//TODO SetRoleIcon
			var addition = ((roleModel.starLevel.Value - 1) * 10) > 5 ? ((roleModel.starLevel.Value - 1) * 10) : 5;
			m_AddtionTxt.text = string.Format("+{0}%", addition);
			m_AddtionTxt.gameObject.SetActive(true);
			m_ItemButton.gameObject.SetActive(true);

		}

        private void OnRoleSelectedItemClick()
        {
			if (m_RoleId > 0) 
			{
				m_SmuggleOrderModel.RemoveRoleModel(m_RoleId);
				m_AddtionTxt.gameObject.SetActive(false);
			}
        }
    }
	
}