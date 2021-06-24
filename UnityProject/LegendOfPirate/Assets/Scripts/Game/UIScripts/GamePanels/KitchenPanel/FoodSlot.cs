using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class FoodSlot : MonoBehaviour
	{
		[SerializeField] private Image m_LockerImg;
		[SerializeField] private TextMeshProUGUI m_CookMsg;
		[SerializeField] private Toggle m_Toggle;

		private KitchenModel m_KitchenModel;
		private FoodSlotModel m_FoodSlotModel;
		private List<IDisposable> m_DisposableList = new List<IDisposable>();
		private ResElementLst m_ResElementLst;

		public void SetSlot(ToggleGroup toggleGroup,int slotCount,ResElementLst resElementLst) 
		{
			m_Toggle.group = toggleGroup;
			m_ResElementLst = resElementLst;

			m_KitchenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Kitchen) as KitchenModel;
			m_FoodSlotModel = m_KitchenModel.foodSlotModelLst[slotCount];
            foreach (var item in m_DisposableList)
            {
                item.Dispose();
            }
            m_DisposableList.Clear();
			m_DisposableList.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValueChange(value)).AddTo(this));
            m_DisposableList.Add(m_FoodSlotModel.isLocked.Subscribe(isLock => SetLocker(isLock)).AddTo(this));
        }

        private void SetLocker(bool isLock)
        {
            m_LockerImg.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_CookMsg.text = !isLock
                ? m_FoodSlotModel.GetFoodName()
                : (string.Format("Kicthen Lv.{0}", m_FoodSlotModel.unLockLevel));
        }

        private void OnToggleValueChange(bool value)
        {
            var SelectedModel = m_KitchenModel.GetSelectModel();
            if (value)
            {

                if (SelectedModel != null)
                {
                    SelectedModel.OnFoodSelect(m_FoodSlotModel.tableConfig.id);
                    m_ResElementLst.SetElement(m_FoodSlotModel.GetResPairs());
                }
                else
                {
                    if (m_KitchenModel.GetAvailableSlot() != null)
                    {
                        m_KitchenModel.GetAvailableSlot().OnFoodSelect(m_FoodSlotModel.tableConfig.id);
                        m_ResElementLst.SetElement(m_FoodSlotModel.GetResPairs());
                    }
                    else
                    {
                        FloatMessageTMP.S.ShowMsg("There is not enough space for cooking");
                    }

                }

            }
            else
            {

                if (SelectedModel != null)
                {
                    SelectedModel.OnFoodUnSelected();
                }
            }
        }
    }
	
}