using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class LaboratorySlot : MonoBehaviour
	{
        [SerializeField] private Image m_IcomImg;
        [SerializeField] private Image m_TimerFillImg;
        [SerializeField] private Image m_Locker;
        [SerializeField] private GameObject m_Timer;

        private LaboratorySlotModel m_LaboratorySlotModel;
        private LaboratoryModel m_LaboratoryModel;
        private List<IDisposable> m_DisposeLst = new List<IDisposable>();

        public void SetSlot(int slotId) 
        {
            m_LaboratoryModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Laboratory) as LaboratoryModel;
            m_LaboratorySlotModel = m_LaboratoryModel.labaratorySlotModelList[slotId];
            foreach (var item in m_DisposeLst)
            {
                item.Dispose();
            }
            m_DisposeLst.Clear();
            BindModelToUI();
        }

        private void BindModelToUI()
        {
            m_DisposeLst.Add(m_LaboratorySlotModel.laboratorySlotState.AsObservable().Subscribe(state => OnSlotStageChange(state)).AddTo(this));
            m_DisposeLst.Add(m_LaboratorySlotModel.makingRemainTime.Where(time => time > 0).Subscribe(time => OnTimerUpdate(time)).AddTo(this));
        }

        private void OnTimerUpdate(float time)
        {
            var makeTime = m_LaboratorySlotModel.GetMakeTime();
            m_TimerFillImg.fillAmount = (makeTime - m_LaboratorySlotModel.makingRemainTime.Value) / makeTime;
        }

        private void OnSlotStageChange(LaboratorySlotState state)
        {
            switch (state)
            {
                case LaboratorySlotState.Free:
                    m_Locker.gameObject.SetActive(false);
                    break;
                case LaboratorySlotState.Making:
                    m_Timer.gameObject.SetActive(true);
                    m_Locker.gameObject.SetActive(false);
                    break;
                case LaboratorySlotState.Locked:
                    m_Locker.gameObject.SetActive(true);
                    break;
                case LaboratorySlotState.Selected:
                    break;
            }
        }
    }
	
}