using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Qarth;
using System;
using UniRx;
namespace GameWish.Game
{
	public class SmuggleOrderItem : MonoBehaviour
	{
		[SerializeField] private GameObject m_SmuggleState;
		[SerializeField] private GameObject m_DoneState;
		[SerializeField] private GameObject m_LockState;

		[SerializeField] private Image m_IconImg;
		[SerializeField] private TextMeshProUGUI m_TimeValueTmp;
		[SerializeField] private TextMeshProUGUI m_PlaceNameTmp;
		[SerializeField] private TextMeshProUGUI m_AwardCount;
		[SerializeField] private TextMeshProUGUI m_AwardAddition;
		[SerializeField] private RectTransform m_RoleLst;

		private SmuggleOrderModel m_SmuggleOrderModel;
		private SmuggleModel m_SmuggleModel;
		private List<IDisposable> m_ListenerList=new List<IDisposable>();
		public void SetInit(int index) 
		{
			foreach (var listener in m_ListenerList) 
			{
				listener.Dispose();
			}
			m_ListenerList.Clear();
			if (m_SmuggleModel == null) m_SmuggleModel = ModelMgr.S.GetModel<SmuggleModel>();
			m_SmuggleOrderModel = m_SmuggleModel.orderModelList[index];
			AddListener();

		}

        private void AddListener()
        {
			m_ListenerList.Add( m_SmuggleOrderModel.orderState.AsObservable().Subscribe(state=>OnOrderStateChang(state)).AddTo(this));

		}

		private void OnOrderStateChang(OrderState state) 
		{
            switch (state)
            {
                case OrderState.Locked:
					m_SmuggleState.SetActive(false);
					m_DoneState.SetActive(false);
					m_LockState.SetActive(true);
					SetLockMsg();
                    break;
                case OrderState.Free:
                    m_SmuggleState.SetActive(true);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
					SetFreeMsg();
					break;
                case OrderState.Smugging:
                    m_SmuggleState.SetActive(true);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
					SetSmggingMsg();
                    break;
                case OrderState.Complate:
                    m_SmuggleState.SetActive(true);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
					SetComplateMsg();
                    break;
                case OrderState.Done:
                    m_SmuggleState.SetActive(false);
                    m_DoneState.SetActive(true);
                    m_LockState.SetActive(false);
					SetDoneMsg();
                    break;
            }
        }

        #region SetStateMsg
        private void SetLockMsg() 
		{
		
		}

		private void SetFreeMsg() 
		{
		
		}

		private void SetSmggingMsg()
		{
		
		}

		private void SetComplateMsg() 
		{
		
		}

		private void SetDoneMsg()
		{
		
		}
        #endregion
    }

}