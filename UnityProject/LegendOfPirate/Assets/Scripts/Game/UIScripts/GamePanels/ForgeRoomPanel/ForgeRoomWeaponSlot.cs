using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;
namespace GameWish.Game
{
	public class ForgeRoomWeaponSlot : MonoBehaviour
	{
		[SerializeField] private Image m_LockerImg;
		[SerializeField] private TextMeshProUGUI m_Label;
		[SerializeField] private Toggle m_Toggle;

		private ForgeRoomModel m_ForgeRoomModel;
		private ForgeWeaponSlotModel m_ForgeWeaponSlotModel;
		private List<IDisposable> m_DisPoseLst = new List<IDisposable>();
		private int m_SlotCount;

		public void SetInit(ToggleGroup togglegroup, int slotcount) 
		{
            m_SlotCount = slotcount;
			m_ForgeRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
			m_ForgeWeaponSlotModel = m_ForgeRoomModel.GetWeaponSlotModel(m_SlotCount);
            foreach (var item in m_DisPoseLst)
            {
                item.Dispose();
            }
            m_DisPoseLst.Clear();
            m_Toggle.group = togglegroup;
            m_DisPoseLst.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValudChange(value)).AddTo(this));
            m_DisPoseLst.Add(m_ForgeWeaponSlotModel.slotIsUnlock.Subscribe(isLock => SetLocker(isLock)).AddTo(this));
        }
        private void SetLocker(bool isLock)
        {
            m_LockerImg.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_Label.text = !isLock
                ? (TDEquipmentSynthesisConfigTable.GetEquipmentSynthesisById( TDFacilityForgeTable.dataList[m_SlotCount].unlockEquipmentID).name) 
                : (string.Format("Garden Lv.{0}", TDFacilityForgeTable.dataList[m_SlotCount].level));
        }

        private void OnToggleValudChange(bool value)
        {
            if (value)
            {
                if (m_ForgeRoomModel.forgeModel.forgeState.Value == ForgeStage.Free)
                    m_ForgeRoomModel.forgeModel.OnWeaponSelect(m_SlotCount);
            }
            else
            {
                if (m_ForgeRoomModel.forgeModel.forgeState.Value == ForgeStage.Select)
                    m_ForgeRoomModel.forgeModel.OnWeaponUnSelect();
            }

        }
    }
	
}