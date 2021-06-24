using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class ProcessingSlot : MonoBehaviour
	{
		[SerializeField] private Image m_IcomImg;
		[SerializeField] private Image m_TimerFillImg;
		[SerializeField] private Image m_Locker;
        [SerializeField] private GameObject m_Timer;
		private ProcessingSlotModel m_ProcessingSlotModel;
		private ProcessingRoomModel m_ProcessingRoomModel;
		private List<IDisposable> m_DisposeLst = new List<IDisposable>();
		public void SetInit(int slotId) 
		{
			m_ProcessingRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ProcessingRoom) as ProcessingRoomModel;
			m_ProcessingSlotModel = m_ProcessingRoomModel.processingSlotModelList[slotId];
			foreach (var item in m_DisposeLst) 
			{
				item.Dispose();
			}
			m_DisposeLst.Clear();
			BindModelToUI();
		}

		private void BindModelToUI() 
		{
            m_DisposeLst.Add(m_ProcessingSlotModel.processState.AsObservable().Subscribe(state => OnSlotStageChange(state)).AddTo(this));
            m_DisposeLst.Add(m_ProcessingSlotModel.ProcessingRemainTime.Where(time => time > 0).Subscribe(timer => OnTimerUpdate(timer)).AddTo(this));
			//m_DisposeLst.Add(m_ProcessingSlotModel.ProcessingRemainTime.Where(time => time <= 0 &&m_ProcessingSlotModel.processState.Value == ProcessSlotState.Processing).Subscribe(_ => OnTimeUp()).AddTo(this));

        }

        //private void OnTimeUp()
        //{
        //    m_ProcessingSlotModel();
        //    m_Timer.gameObject.SetActive(false);
        //}

        private void OnTimerUpdate(float time)
        {
            var makeTime = m_ProcessingSlotModel.GetMakeTime();
            m_TimerFillImg.fillAmount = (makeTime- m_ProcessingSlotModel.ProcessingRemainTime.Value) / makeTime;
        }

        private void OnSlotStageChange(ProcessSlotState state) 
		{

            switch (state)
            {
                case ProcessSlotState.Free:
                    m_Locker.gameObject.SetActive(false);
                    break;
                case ProcessSlotState.Processing:
                    m_Timer.gameObject.SetActive(true);
                    m_Locker.gameObject.SetActive(false);
                    break;
                case ProcessSlotState.Locked:
                    m_Locker.gameObject.SetActive(true);
                    break;
                case ProcessSlotState.Selected:
                    break;
            }
        }

    }

}