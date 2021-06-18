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
        private IDisposable m_ImgStateSub;
        private IDisposable m_TrainStateSub;
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
            BindModelToUI();

            //ChangeBtnState();
        }

        private void BindModelToUI()
        {
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                EventSystem.S.Send(EventID.OnTrainingSelectRole, m_BottomTrainingData);
            }).AddTo(this);

            m_ImgStateSub = m_BottomTrainingData.isSelected.Subscribe(val =>
            {
                m_State.gameObject.SetActive(val);
            });

            BindTrainState();
        }
        #region IItemCom
        public void OnReset()
        {
            m_TrainStateSub?.Dispose();
            m_ImgStateSub?.Dispose();
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
        }

        public void HandleSelectedRole(bool select, TrainingSlotModel trainingSlotModel = null)
        {
            m_BottomTrainingData.isSelected.Value = !m_BottomTrainingData.isSelected.Value;
            m_ImgStateSub?.Dispose();
            m_BottomTrainingData.trainingSlotModel = trainingSlotModel;
            if (select)
            {
                BindTrainState();
            }
        }

        private void BindTrainState()
        {
            m_ImgStateSub = m_BottomTrainingData.trainingSlotModel?.trainState.Subscribe(val =>
            {
                switch (val)
                {
                    case TrainingSlotState.Free:
                        m_BottomTrainingRole.enabled = true;
                        m_State.gameObject.SetActive(false);
                        break;
                    case TrainingSlotState.Training:
                        m_BottomTrainingRole.enabled = false;
                        m_State.gameObject.SetActive(true);
                        break;
                }
            });
        }

        private void ChangeBtnState()
        {
            m_BottomTrainingRole.enabled = !m_BottomTrainingData.isSelected.Value;
        }
        #endregion
    }
}