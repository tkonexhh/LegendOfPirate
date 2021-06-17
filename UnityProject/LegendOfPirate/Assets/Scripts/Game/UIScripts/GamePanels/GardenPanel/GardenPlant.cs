using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace GameWish.Game
{
	public class GardenPlant : MonoBehaviour
	{
		[SerializeField] private Toggle m_Toggle;
		[SerializeField] private Image m_Locker;
		[SerializeField] private TextMeshProUGUI m_Label;
		public void SetInit(ToggleGroup togglegroup, int slotcount) 
		{
			m_Toggle.group = togglegroup;
			var gardenmodel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Garden);
			var lockstage = TDFacilityGardenTable.dataList[slotcount].level > gardenmodel.level.Value;
			m_Locker.gameObject.SetActive(lockstage);
			m_Toggle.interactable = !lockstage;

			m_Label.text = lockstage ? (TDFacilityGardenTable.dataList[slotcount].seedUnlock) : (string.Format("Garden Lv.{0}", TDFacilityGardenTable.dataList[slotcount].level));
		}
	}	
}