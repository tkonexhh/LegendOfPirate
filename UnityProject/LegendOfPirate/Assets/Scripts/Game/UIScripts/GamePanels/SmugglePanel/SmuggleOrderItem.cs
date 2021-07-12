using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Qarth;
using System;
using UniRx;
using System.Linq;
namespace GameWish.Game
{
    public class SmuggleOrderItem : MonoBehaviour
    {
        [SerializeField] private GameObject m_SmuggleState;
        [SerializeField] private GameObject m_DoneState;
        [SerializeField] private GameObject m_LockState;
        [SerializeField] private GameObject m_ComplateState;

        [SerializeField] private Image m_IconImg;
        [SerializeField] private TextMeshProUGUI m_TimeValueTmp;
        [SerializeField] private TextMeshProUGUI m_PlaceNameTmp;
        [SerializeField] private TextMeshProUGUI m_AwardCount;
        [SerializeField] private TextMeshProUGUI m_AwardAddition;
        [SerializeField] private RectTransform m_RoleLstTrs;
        [SerializeField] private Button m_ChoiceRoleBtn;

        private SmuggleOrderModel m_SmuggleOrderModel;
        private SmuggleModel m_SmuggleModel;
        private List<IDisposable> m_ListenerList = new List<IDisposable>();
        private List<SmuggleRoleItem> m_RoleItemList = new List<SmuggleRoleItem>();
        private bool Islocked = true;
        private int m_OrderId;
        public void SetInit(int index)
        {
            foreach (var listener in m_ListenerList)
            {
                listener.Dispose();
            }
            m_ListenerList.Clear();
            if (m_SmuggleModel == null) m_SmuggleModel = ModelMgr.S.GetModel<SmuggleModel>();
            m_SmuggleOrderModel = m_SmuggleModel.orderModelList[index];
      
            var roleitems = m_RoleLstTrs.GetComponentsInChildren<SmuggleRoleItem>();
            foreach (var item in roleitems)
            {
                m_RoleItemList.Add(item);
            }

            m_OrderId = 1+index;

            AddListener();

        }

        private void AddListener()
        {
            m_ListenerList.Add(m_SmuggleOrderModel.isLocked.AsObservable().Where(islocked =>!islocked).Subscribe(_ => UnlockOrder()).AddTo(this));
            m_ListenerList.Add(m_SmuggleOrderModel.orderState.AsObservable().Subscribe(state => OnOrderStateChang(state)).AddTo(this));
            m_ListenerList.Add(m_SmuggleOrderModel.needRefresh.AsObservable().Where(needRresh => needRresh).Subscribe(_ => ReFreshRoleLst()).AddTo(this));
            m_ListenerList.Add(m_ChoiceRoleBtn.OnClickAsObservable().Subscribe(_ => UIMgr.S.OpenPanel(UIID.SmuggleChooseRolePanel, m_OrderId)).AddTo(this));

        }

        private void UnlockOrder()
        {
            m_SmuggleState.SetActive(false);
            m_DoneState.SetActive(false);
            m_LockState.SetActive(true);
            m_ComplateState.SetActive(false);
            Islocked = false;
        }

        private void ReFreshRoleLst()
        {
            int totalAddition = 0;
            for (int i = 0; i < m_SmuggleOrderModel.roleList.Count; i++)
            {
                m_RoleItemList[i].SetRoleItemData(m_SmuggleOrderModel.roleList[i]);
                totalAddition += m_RoleItemList[i].roleAddttion;
            }
            m_AwardAddition.text = string.Format("+{0}%", totalAddition);

        }


        private void OnOrderStateChang(OrderState state)
        {
            if (Islocked) return;
            switch (state)
            {
                case OrderState.Free:
                    m_SmuggleState.SetActive(true);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
                    m_ComplateState.SetActive(false);
                    SetFreeMsg();
                    break;
                case OrderState.Smugging:
                    m_SmuggleState.SetActive(true);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
                    m_ComplateState.SetActive(false);
                    SetSmggingMsg();
                    break;
                case OrderState.Complate:
                    m_SmuggleState.SetActive(false);
                    m_DoneState.SetActive(false);
                    m_LockState.SetActive(false);
                    m_ComplateState.SetActive(true);
                    SetComplateMsg();
                    break;
                case OrderState.Done:
                    m_SmuggleState.SetActive(false);
                    m_DoneState.SetActive(true);
                    m_LockState.SetActive(false);
                    m_ComplateState.SetActive(false);
                    break;
            }
        }

        #region SetStateMsg

        private void SetFreeMsg()
        {

        }

        private void SetSmggingMsg()
        {

        }

        private void SetComplateMsg()
        {

        }

        #endregion
    }

}