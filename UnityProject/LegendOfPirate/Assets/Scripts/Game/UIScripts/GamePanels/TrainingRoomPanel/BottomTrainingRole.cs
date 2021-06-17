using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

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
        private IDisposable m_StateSubscribe;
        #endregion

        public void OnInit(BottomRoleModel bottomTrainingRoleData)
        {
            OnReset();
            if (bottomTrainingRoleData == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }
            m_BottomTrainingData = bottomTrainingRoleData;
            BindPropToUI();

            ChangeBtnState();
        }

        private void BindPropToUI()
        {
            m_StateSubscribe = m_BottomTrainingData.isSelected.Subscribe(val =>
            {
                m_State.gameObject.SetActive(val);
            });
        }
        #region IItemCom
        public void OnReset()
        {
            m_StateSubscribe?.Dispose();
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                EventSystem.S.Send(EventID.OnTrainingSelectRole, m_BottomTrainingData);
            }).AddTo(this);
        }

        public void HandleSelectedRole()
        {
            m_BottomTrainingData.isSelected.Value = !m_BottomTrainingData.isSelected.Value;
        }

        private void ChangeBtnState()
        {
            m_BottomTrainingRole.enabled = !m_BottomTrainingData.isSelected.Value;
        }
        #endregion
    }
}