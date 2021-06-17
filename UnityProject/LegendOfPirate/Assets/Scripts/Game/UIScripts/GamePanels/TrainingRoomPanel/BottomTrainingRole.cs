using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace GameWish.Game
{
    public class BottomTrainingRole : UListItemView
    {
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;

        #region Data
        private BottomRoleModel m_BottomTrainingData;
        private IntReactiveProperty m_IntReactiveSelectedCount;
        #endregion

        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
                case EventID.OnTRoomStartTraining:
                    if (m_BottomTrainingData==null)
                        return;
                    if (m_BottomTrainingData.roleID.Value == (int)(param[0]))
                    {
                        //m_BottomTrainingRole.gameObject.SetActive(false);
                    }
                    break;

            }
        }

        private void Start()
        {
            EventSystem.S.Register(EventID.OnTRoomStartTraining, HandlerEvent);
        }

        private void OnDestroy()
        {
            EventSystem.S.UnRegister(EventID.OnTRoomStartTraining, HandlerEvent);

        }

        public void OnInit(BottomRoleModel bottomTrainingRoleData, IntReactiveProperty selectedCount)
        {
            OnReset();
            if (bottomTrainingRoleData == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }
            m_BottomTrainingData = bottomTrainingRoleData;
            m_IntReactiveSelectedCount = selectedCount;

            OnRefresh();
        }
        #region IItemCom
        public void OnReset()
        {
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                if (m_BottomTrainingData.bottomTrainingRole)
                {

                }
                EventSystem.S.Send(EventID.OnTrainingSelectRole, m_BottomTrainingData);
            }).AddTo(this);
        }

        public void HandleSelectedRole()
        {
            m_BottomTrainingData.isSelected.Value = !m_BottomTrainingData.isSelected.Value;
            OnRefresh();
        }

        public void OnRefresh()
        {
            if (m_BottomTrainingData.isSelected.Value)
            {
                m_State.gameObject.SetActive(true);
                m_BottomTrainingRole.enabled = false;
            }
            else
            {
                m_State.gameObject.SetActive(false);
                m_BottomTrainingRole.enabled = true;
            }
        }
        #endregion
    }

}