using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class KitchenSlot : MonoBehaviour
	{
        [SerializeField] private Image m_IcomImg;
        [SerializeField] private Image m_TimerFillImg;
        [SerializeField] private Image m_Locker;
        [SerializeField] private GameObject m_Timer;

        private int m_SlotId;
        private KitchenSlotModel m_KitchenSlotModel;
        private KitchenModel m_KitchenModel;
        private List<IDisposable> m_DisposeLst = new List<IDisposable>();

        public void SetSlot(int slotId) 
        {
            m_KitchenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Kitchen) as KitchenModel;
            m_KitchenSlotModel = m_KitchenModel.kitchenSlotModelLst[slotId];
            foreach (var item in m_DisposeLst)
            {
                item.Dispose();

            }
            m_DisposeLst.Clear();
            BindModelToUI();
        }

        private void BindModelToUI()
        {
            m_DisposeLst.Add(m_KitchenSlotModel.kitchenSlotState.AsObservable().Subscribe(state => OnSlotStageChange(state)).AddTo(this));
            m_DisposeLst.Add(m_KitchenSlotModel.cookingRemainTime.Where(time => time > 0).Subscribe(timer => OnTimerUpdate(timer)).AddTo(this));
        }

        private void OnTimerUpdate(float timer)
        {
            var makeTime = m_KitchenSlotModel.GetMakeTime();
            m_TimerFillImg.fillAmount = (makeTime - m_KitchenSlotModel.cookingRemainTime.Value) / makeTime;
        }

        private void OnSlotStageChange(KitchenSlotState state)
        {
            switch (state)
            {
                case KitchenSlotState.Free:
                    m_Locker.gameObject.SetActive(false);
                    break;
                case KitchenSlotState.Cooking:
                    m_Timer.gameObject.SetActive(true);
                    m_Locker.gameObject.SetActive(false);
                    break;
                case KitchenSlotState.Locked:
                    m_Locker.gameObject.SetActive(true);
                    break;
                case KitchenSlotState.Selected:
                    break;
            }
        }
    }

}