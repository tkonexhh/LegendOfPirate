using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class PotionSlot : MonoBehaviour
	{
        [SerializeField] private Image m_LockerImg;
        [SerializeField] private TextMeshProUGUI m_Label;
        [SerializeField] private Toggle m_Toggle;

        private LaboratoryModel m_LaboratoryModel;
        private PotionSlotModel m_PotionSlotModel;
        private List<IDisposable> m_DisposableList = new List<IDisposable>();
        private ResElementLst m_ResElementLst;

        public void SetSlot(ToggleGroup toggleGroup, int slotCount, ResElementLst elementLst)
        {
            m_Toggle.group = toggleGroup;
            m_ResElementLst = elementLst;

            m_LaboratoryModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Laboratory) as LaboratoryModel;
            m_PotionSlotModel = m_LaboratoryModel.potionSlotModelList[slotCount];
            foreach (var item in m_DisposableList)
            {
                item.Dispose();
            }
            m_DisposableList.Clear();
            m_DisposableList.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValudChange(value)).AddTo(this));
            m_DisposableList.Add(m_PotionSlotModel.isLocked.Subscribe(isLock => SetLocker(isLock)).AddTo(this));

        }

        private void SetLocker(bool isLock)
        {
            m_LockerImg.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_Label.text = !isLock
                ? m_PotionSlotModel.GetPotionName()
                : (string.Format("Laboratory Lv.{0}", m_PotionSlotModel.unLockLevel));
        }

        private void OnToggleValudChange(bool value)
        {
            var SelectedModel = m_LaboratoryModel.GetSlotModel(LaboratorySlotState.Selected);
            if (value)
            {

                if (SelectedModel != null)
                {
                    SelectedModel.OnPartSelected(m_PotionSlotModel.potionConfig.id);
                    m_ResElementLst.SetElement(m_PotionSlotModel.GetResPairs());
                }
                else
                {
                    if (m_LaboratoryModel.GetSlotModel() != null)
                    {
                        m_LaboratoryModel.GetSlotModel().OnPartSelected(m_PotionSlotModel.potionConfig.id);
                        m_ResElementLst.SetElement(m_PotionSlotModel.GetResPairs());
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
                    SelectedModel.OnPartUnSelected();
                }
            }
        }

    }
	
}