using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using Qarth;

namespace GameWish.Game
{
	public enum EquipSubpartState 
	{
	
	}
	public class EquipSubpart : MonoBehaviour 
	{
		[SerializeField] private Button m_EquipButton;
		[SerializeField] private IUListView m_ListView;
		[SerializeField] private Image m_EquipImg;
		private int m_EquipId;
		private RoleEquipModel m_RoleEquipModel;
		private IDisposable m_CurBtnClickEvent;
		private int m_RoleId;
		private EquipmentType m_EquipmentType;

		public bool IsLocked = true;
		
		public void InitEquipSubpart(int roleId,EquipmentType equipmentType) 
		{
			SetData(roleId, equipmentType);
			SetUI();
		}

		public void UpdateEquipSubpart(RoleEquipModel roleEquipModel) 
		{
			if (!IsLocked) return;
			if (roleEquipModel == null) return;
            m_EquipId = m_RoleEquipModel.equipId;
            IsLocked = false;
            m_RoleEquipModel = roleEquipModel;
			SetUI();
        }

		private void SetData(int roleId, EquipmentType equipmentType) 
		{
            m_RoleId = roleId;
            var roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(roleId);
            m_EquipId = roleModel.GetEquipModelIdByType(equipmentType);
            IsLocked = !roleModel.equipDic.ContainsKey(equipmentType);
            m_EquipmentType = equipmentType;
            m_RoleEquipModel = roleModel.GetEquipModelByType(equipmentType);
        }

		private void SetUI() 
		{
			if (!IsLocked)
			{
				if(m_CurBtnClickEvent!=null) m_CurBtnClickEvent.Dispose();
				m_EquipImg.gameObject.SetActive(true);
				m_ListView.gameObject.SetActive(true);
				m_RoleEquipModel.equipLevel.AsObservable().Subscribe(level => SetEquipStar(level / 5)).AddTo(this);
				switch (m_RoleEquipModel.equipRarity)
				{
					//TODO ButtonImage TO EquipRarity
					case EquipQualityType.Normal:
						break;
					case EquipQualityType.Advanced:
						break;
					case EquipQualityType.Rare:
						break;
					case EquipQualityType.Epic:
						break;
					case EquipQualityType.Legendary:
						break;
					case EquipQualityType.Immortal:
						break;
				}
				m_CurBtnClickEvent = m_EquipButton.OnClickAsObservable().Subscribe(_ => ShowEquipMsg()).AddTo(this);
				//TODO SetEquipImage
			}
			else 
			{
				if (m_CurBtnClickEvent != null) m_CurBtnClickEvent.Dispose();
				m_EquipImg.gameObject.SetActive(false);
				m_ListView.gameObject.SetActive(false);
				m_CurBtnClickEvent = m_EquipButton.OnClickAsObservable().Subscribe(_ => ShowEquipGet()).AddTo(this);
				//TODO SetButtonImage TO AddEquip
			}

		}
        private void ShowEquipMsg()
        {
			//TODO ShowEquipMsg
			UIMgr.S.OpenPanel(UIID.RoleEquipDetailsPanel,m_RoleId,m_EquipmentType);
		}

		private void ShowEquipGet() 
		{
			UIMgr.S.OpenPanel(UIID.RoleEquipGetPanel, m_EquipmentType,m_RoleId);
		}
        private void SetEquipStar(int starCount)
		{
			m_ListView.SetDataCount(starCount);
		}
		
		
		
    }

}