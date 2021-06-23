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
		private ForgeEquipmentSlotModel m_ForgeWeaponSlotModel;
		private List<IDisposable> m_DisposeLst = new List<IDisposable>();
		private int m_SlotCount;

		public void SetInit(ToggleGroup togglegroup, int slotcount) 
		{
            m_SlotCount = slotcount;
			m_ForgeRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
			m_ForgeWeaponSlotModel = m_ForgeRoomModel.GetWeaponSlotModel(m_SlotCount);
            foreach (var item in m_DisposeLst)
            {
                item.Dispose();
            }
            m_DisposeLst.Clear();
            m_Toggle.group = togglegroup;
            m_DisposeLst.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValudChange(value)).AddTo(this));
            m_DisposeLst.Add(m_ForgeWeaponSlotModel.slotIsUnlock.Subscribe(isLock => SetLocker(isLock)).AddTo(this));
        }
        private void SetLocker(bool isLock)
        {
            m_LockerImg.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_Label.text = !isLock
                ?  m_ForgeWeaponSlotModel.equipmentName
                : (string.Format("Forge Lv.{0}",m_ForgeWeaponSlotModel.unlockLevel));
        }

        private void OnToggleValudChange(bool value)
        {
            if (value)
            {
                if (m_ForgeRoomModel.forgeModel.forgeState.Value == ForgeStage.Free)
                    m_ForgeRoomModel.forgeModel.OnWeaponSelect(TDFacilityForgeTable.dataList[m_SlotCount].unlockEquipmentID);
            }
            else
            {
                if (m_ForgeRoomModel.forgeModel.forgeState.Value == ForgeStage.Select)
                    m_ForgeRoomModel.forgeModel.OnWeaponUnSelect();
            }
        }
    }
	
}