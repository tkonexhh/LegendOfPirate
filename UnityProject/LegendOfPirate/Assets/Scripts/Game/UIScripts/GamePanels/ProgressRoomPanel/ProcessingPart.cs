using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class ProcessingPart : MonoBehaviour
	{
		[SerializeField] private Image m_LockerImg;
        [SerializeField] private TextMeshProUGUI m_Label;
        [SerializeField] private Toggle m_Toggle;

		private ProcessingRoomModel m_ProcessingRoomModel;
		private ProcessingPartModel m_ProcessingPartModel;
		private List<IDisposable> m_DisposableList = new List<IDisposable>();
        private ResElementLst m_ResElementLst;
		private int m_SlotCount;
		public void SetInit(ToggleGroup toggleGroup, int slotCount,ResElementLst elementLst) 
		{
			m_Toggle.group = toggleGroup;
			m_SlotCount = slotCount;
            m_ResElementLst = elementLst;

			m_ProcessingRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ProcessingRoom) as ProcessingRoomModel;
			m_ProcessingPartModel = m_ProcessingRoomModel.ProcessingPartModelList[slotCount];
			foreach (var item in m_DisposableList) 
			{
				item.Dispose();
			}
			m_DisposableList.Clear();
            m_DisposableList.Add(m_Toggle.OnValueChangedAsObservable().Subscribe(value => OnToggleValudChange(value)).AddTo(this));
            m_DisposableList.Add(m_ProcessingPartModel.isLocked.Subscribe(isLock => SetLocker(isLock)).AddTo(this));
       
		}

        private void SetLocker(bool isLock)
        {
            m_LockerImg.gameObject.SetActive(isLock);
            m_Toggle.interactable = !isLock;
            m_Label.text = !isLock
                ?  m_ProcessingPartModel.GetPartName()
                : (string.Format("ProcessingRoom Lv.{0}", m_ProcessingPartModel.unlockLevel));
        }

        private void OnToggleValudChange(bool value)
        {
            var SelectedModel = m_ProcessingRoomModel.GetSelectModel();
            if (value)
            {
              
                if (SelectedModel != null)
                {
                    SelectedModel.OnPartSelected(m_ProcessingPartModel.partConfig.id);
                    m_ResElementLst.SetElement(m_ProcessingPartModel.GetResPairs());
                }
                else
                {
                    if (m_ProcessingRoomModel.GetAvailableSlot() != null)
                    {
                        m_ProcessingRoomModel.GetAvailableSlot().OnPartSelected(m_ProcessingPartModel.partConfig.id);
                        m_ResElementLst.SetElement(m_ProcessingPartModel.GetResPairs());
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