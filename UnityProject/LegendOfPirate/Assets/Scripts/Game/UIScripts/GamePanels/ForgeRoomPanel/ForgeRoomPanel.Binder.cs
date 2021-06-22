using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

namespace GameWish.Game
{
	public class ForgePanelData : UIPanelData
	{
		public ForgeRoomModel forgeRoomModel;
		public List<ForgeRoomWeaponSlot> lockerList;
		public ForgePanelData()
		{
			lockerList = new List<ForgeRoomWeaponSlot>();
		}
		public int GetSlotCount() 
		{
			return forgeRoomModel.forgeWeaponSlotModels.Count;
		}
	}
	
	public partial class ForgeRoomPanel
	{
		private ForgePanelData m_PanelData = null;

		private void AllocatePanelData(params object[] args)
		{
			m_PanelData = UIPanelData.Allocate<ForgePanelData>();
			m_PanelData.forgeRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
			var plantSlots = m_Content.GetComponentsInChildren<Toggle>();
          
        }

	    private void ReleasePanelData()
		{
			ObjectPool<ForgePanelData>.S.Recycle(m_PanelData);
			m_PanelData.lockerList.Clear();
		}
		
		private void BindModelToUI()
		{
			m_PanelData.forgeRoomModel.level.SubscribeToTextMeshPro(m_BuildingLevel,"Lv.{0}").AddTo(this);
			m_PanelData.forgeRoomModel.level.Subscribe(level => OnBuildingLevelUp(level)).AddTo(this);
			m_PanelData.forgeRoomModel.forgeModel.forgeState.AsObservable().Subscribe(state =>OnForgeStateChange(state)).AddTo(this);
			m_PanelData.forgeRoomModel.forgeModel.equipmentId.Subscribe(equipmentid =>OnSelectEquipmentChange(equipmentid)).AddTo(this);
			m_PanelData.forgeRoomModel.forgeModel.forgeRemainTime.Where(time => time > 0).Subscribe(timer =>OnTimerUpdate(timer)).AddTo(this);
			m_PanelData.forgeRoomModel.forgeModel.forgeRemainTime.Where(time => time <= 0 && m_PanelData.forgeRoomModel.forgeModel.forgeState.Value == ForgeStage.Forging).Subscribe(_ => OnTimeUp()).AddTo(this);
		}
		
		private void BindUIToModel()
		{

            
        }

		private void InitPanelBtn()
		{
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelBtnClick()).AddTo(this);
            m_ForgeBtn.OnClickAsObservable().Subscribe(_ => OnForgeBtnClick()).AddTo(this);
        }

		private void OnForgeBtnClick()
        {
            switch (m_PanelData.forgeRoomModel.forgeModel.forgeState.Value)
            {
                case ForgeStage.Free:
					FloatMessageTMP.S.ShowMsg("Select Equipment");
					break;
                case ForgeStage.Select:
					m_PanelData.forgeRoomModel.forgeModel.OnStartForge(DateTime.Now);
					var slots =m_Content.GetComponentsInChildren<Toggle>();
					foreach (var item in slots) 
					{
						item.isOn = false;
					}
					m_Timer.gameObject.SetActive(true);
					break;
                case ForgeStage.ForgeComplate:
					m_PanelData.forgeRoomModel.forgeModel.OnGetWeapon();
                    break;
            }
        }
        private void OnLevelBtnClick()
        {
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.ForgeRoom);
        }

        private void OnBuildingLevelUp(int level) 
		{

		}

		private void OnSelectEquipmentChange(int equipmentId) 
		{
			var equipmemtMsg = m_PanelData.forgeRoomModel.forgeModel.makeEquipmentMsgModel;
			m_WeaponName.text = equipmemtMsg.equipmentName;
			m_ForgeElement1.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("0/{0}", equipmemtMsg.makeResList[0].resCount);
			m_ForgeElement2.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("0/{0}", equipmemtMsg.makeResList[1].resCount);
			m_WeaponMsg.text = equipmemtMsg.equipmentConfig.desc;
		}
		public void OnForgeStateChange(ForgeStage forgestage) 
		{
            switch (forgestage)
            {
                case ForgeStage.Free:
					m_ForgeBtn.interactable = true;
					m_ForgeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Forge";
					break;
                case ForgeStage.Forging:
					m_ForgeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Forging";
					m_ForgeBtn.interactable = false;
					m_Timer.gameObject.SetActive(true);
					break;
                case ForgeStage.Select:
                    m_ForgeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Forge";
                    m_ForgeBtn.interactable = true;
                    break;
                case ForgeStage.ForgeComplate:
                    m_ForgeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "GetEquipment";
                    m_ForgeBtn.interactable = true;
                    break;
            }
        }

		private void OnTimerUpdate(float time) 
		{
			m_TimerFill.fillAmount = (m_PanelData.forgeRoomModel.forgeModel.makeEquipmentMsgModel.makeTime-time) /
			m_PanelData.forgeRoomModel.forgeModel.makeEquipmentMsgModel.makeTime;
			m_TimerText.text = string.Format("{0:f2}",  time);
		}
		private void OnTimeUp() 
		{
			m_PanelData.forgeRoomModel.forgeModel.OnForgeFinish();
			m_Timer.gameObject.SetActive(false);
		}
    }
}
