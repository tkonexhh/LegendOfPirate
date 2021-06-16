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
        private BottomTrainingModel m_BottomTrainingData;
        private IntReactiveProperty m_IntReactiveSelectedCount;
        #endregion

        private void Awake()
        {
        }

        public void OnInit(BottomTrainingModel bottomTrainingRoleData, IntReactiveProperty selectedCount)
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
                EventSystem.S.Send(EventID.OnTrainingRoomSelectRole, m_BottomTrainingData);
            });
        }

        public void HandleSelectedRole()
        {
            m_BottomTrainingData.isSelected.Value = !m_BottomTrainingData.isSelected.Value;
            m_IntReactiveSelectedCount.Value += (m_BottomTrainingData.isSelected.Value ? 1 : -1);
            OnRefresh();
        }

        public void OnRefresh()
        {
            if (m_BottomTrainingData.isSelected.Value)
            {
                m_State.gameObject.SetActive(true);
            }
            else
            {
                m_State.gameObject.SetActive(false);
            }
        }
        #endregion
    }

}