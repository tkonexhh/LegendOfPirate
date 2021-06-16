﻿using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using GFrame;
using TMPro;
using UnityEngine;

namespace GameWish.Game
{
	public class ForgePanelData : UIPanelData
	{
		public ForgeRoomModel forgeModel;
		public List<GameObject> lockerList;
		public ForgePanelData()
		{
			lockerList = new List<GameObject>();
		}
	}
	
	public partial class ForgePanel
	{
		private ForgePanelData m_PanelData = null;

		private void AllocatePanelData(params object[] args)
		{
			m_PanelData = UIPanelData.Allocate<ForgePanelData>();
			m_PanelData.forgeModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
			var plantSlots = Content.GetComponentsInChildren<Toggle>();
            foreach (var item in plantSlots)
            {
                var Locker = item.GetComponentInChildren<GImage>().gameObject;
                m_PanelData.lockerList.Add(Locker);
            }
        }

	    private void ReleasePanelData()
		{
			ObjectPool<ForgePanelData>.S.Recycle(m_PanelData);
			m_PanelData.lockerList.Clear();
		}
		
		private void BindModelToUI()
		{
			m_PanelData.forgeModel.level.SubscribeToTextMeshPro(BuildingLevel,"Lv.{0}").AddTo(this);
			m_PanelData.forgeModel.level.Subscribe(level => OnBuildingLevelUp(level)).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			LevelUpBtn.OnClickAsObservable().Subscribe(_=>OnLevelBtnClick()).AddTo(this);
			ForgeBtn.OnClickAsObservable().Subscribe(_ => OnForgeBtnClick()).AddTo(this);
            
        }

        private void OnForgeBtnClick()
        {
            
        }
        private void OnLevelBtnClick()
        {
        
                UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.ForgeRoom);
        }

        private void OnBuildingLevelUp(int level) 
		{
			SetUnlockedToggle(level);
		}
		private void SetUnlockedToggle(int level) 
		{
			var toggles = Content.GetComponentsInChildren<Toggle>();
			for (int i = 0; i < toggles.Length; i++) 
			{

                if (level >= TDFacilityForgeTable.dataList[i].level)
                {
					
					m_PanelData.lockerList[i].SetActive(false);
					toggles[i].GetComponentInChildren<TextMeshProUGUI>().text = TDFacilityForgeTable.dataList[i].unlockEquipmentID.ToString();
                }
                else
                {
					toggles[i].interactable = false;
					toggles[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Format("ForgeLevel {0}", TDFacilityForgeTable.dataList[i].level);
                }
            }
		}
	}
}
