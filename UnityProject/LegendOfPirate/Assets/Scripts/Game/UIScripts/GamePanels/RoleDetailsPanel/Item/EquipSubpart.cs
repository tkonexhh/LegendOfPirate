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

		public bool IsLocked = true;
		
		public void InitEquipSubpart(RoleEquipModel roleEquipModel) 
		{

			if (roleEquipModel != null)
			{
				m_EquipId = m_RoleEquipModel.equipId;
				IsLocked = false;
			}
			else 
			{
				m_EquipId = -1;			
			}
			m_RoleEquipModel = roleEquipModel;
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
	
		private void SetUI() 
		{
			if (m_EquipId!= -1)
			{
				if(m_CurBtnClickEvent!=null) m_CurBtnClickEvent.Dispose();
				m_EquipImg.gameObject.SetActive(true);
				m_ListView.gameObject.SetActive(true);
				m_RoleEquipModel.equipLevel.AsObservable().Subscribe(level => SetEquipStar(level / 5)).AddTo(this);
				switch (m_RoleEquipModel.equipRarity)
				{
					//TODO ButtonImage TO EquipRarity
					case EquipRarity.Normal:
						break;
					case EquipRarity.Advanced:
						break;
					case EquipRarity.Rare:
						break;
					case EquipRarity.Epic:
						break;
					case EquipRarity.Legendary:
						break;
					case EquipRarity.Immortal:
						break;
				}
				//TODO SetEquipImage
				m_CurBtnClickEvent = m_EquipButton.OnClickAsObservable().Subscribe(_=>ShowEquipMsg()).AddTo(this);
			}
			else 
			{
				if (m_CurBtnClickEvent != null) m_CurBtnClickEvent.Dispose();
				m_EquipImg.gameObject.SetActive(false);
				m_ListView.gameObject.SetActive(false);
				m_CurBtnClickEvent = m_EquipButton.OnClickAsObservable().Subscribe(_ => OpenAddEquipPanel()).AddTo(this);
				//TODO SetButtonImage TO AddEquip
			}
        }

        private void OpenAddEquipPanel()
        {
            //TODO OpenAddEquipPanel
        }

        private void ShowEquipMsg()
        {
           //TODO ShowEquipMsg
        }

        private void SetEquipStar(int starCount)
		{
			m_ListView.SetDataCount(starCount);
		}
		
		
		
    }

}