using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;
namespace GameWish.Game
{
	public class GardenPlantSlot : MonoBehaviour
	{
		[SerializeField] private Toggle m_Toggle;
		[SerializeField] private Image m_Locker;
		[SerializeField] private TextMeshProUGUI m_Label;

		private GardenModel m_GardenModel;
		private PlantSlotModel m_PlantSlotModel;
		private List<IDisposable> m_DisPoseLst = new List<IDisposable>();
		private int m_SlotCount;
		
		public void SetInit(ToggleGroup togglegroup, int slotcount) 
		{
			m_SlotCount = slotcount;
			m_GardenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Garden) as GardenModel;
			m_PlantSlotModel = m_GardenModel.GetPlantSlotModel(m_SlotCount);
			foreach (var item in m_DisPoseLst) 
			{
				item.Dispose();
			}
			m_DisPoseLst.Clear();
			m_Toggle.group = togglegroup;
			m_DisPoseLst.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValudChange(value)).AddTo(this));
			m_DisPoseLst.Add(m_PlantSlotModel.slotIsUnlock.Subscribe(isLock => SetLocker(isLock)).AddTo(this));
		
		}

		private void SetLocker(bool isLock) 
		{
            m_Locker.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_Label.text = !isLock ? (TDFacilityGardenTable.dataList[m_SlotCount].seedUnlock) : (string.Format("Garden Lv.{0}", TDFacilityGardenTable.dataList[m_SlotCount].level));
        }

		private void OnToggleValudChange(bool value) 
		{
			if (value)
			{
				if(m_GardenModel.gardenPlantModel.gardenState.Value==GardenState.Free)
				m_GardenModel.gardenPlantModel.OnPlantSelect(m_SlotCount);
			}
			else
            {
                if (m_GardenModel.gardenPlantModel.gardenState.Value == GardenState.Select)
                    m_GardenModel.gardenPlantModel.OnPlantUnSelect();
			}

		}
	}	
}