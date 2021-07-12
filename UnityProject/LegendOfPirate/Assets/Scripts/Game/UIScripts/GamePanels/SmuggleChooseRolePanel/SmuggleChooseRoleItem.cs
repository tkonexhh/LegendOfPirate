using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class SmuggleChooseRoleItem : MonoBehaviour
	{
		[SerializeField] private Button m_Button;
		[SerializeField] private Image m_Image;
		private int m_RoleIndex;
		private RoleModel m_RoleModel;
		private SmuggleOrderModel m_SmuggleOrderModel;
		public void InitItem(int roleIndex,SmuggleOrderModel  smuggleOrderModel) 
		{
			m_RoleIndex = roleIndex;
			m_RoleModel = ModelMgr.S.GetModel<RoleGroupModel>().roleItemList[m_RoleIndex];
			m_SmuggleOrderModel = smuggleOrderModel;
			SetItemMsg();
		}

		public void SetItemMsg() 
		{
			m_Button.onClick.RemoveAllListeners();
			m_Button.onClick.AddListener(OnRoleButonClick);
		}

        private void OnRoleButonClick()
        {
			m_RoleModel.SelectToSmuggle();
	
			m_SmuggleOrderModel.AddRoleModel(m_RoleModel.id);
        }
    }
	
}