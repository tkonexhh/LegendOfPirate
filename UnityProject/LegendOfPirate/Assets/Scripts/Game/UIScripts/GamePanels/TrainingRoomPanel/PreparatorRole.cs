using Qarth;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
    public class PreparatorRole : UListItemView
    {
        [SerializeField]
        private Button m_PrepRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;

        #region Data
        private PreparatorRoleModel m_PrepRoleModel;
        private IDisposable m_ImgStateSub;
        private IDisposable m_TrainStateSub;
        #endregion

        public void OnRefresh(PreparatorRoleModel prepRoleModel)
        {
            OnReset();
            if (prepRoleModel == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }
            m_PrepRoleModel = prepRoleModel;
            BindModelToUI();
        }
        private void BindModelToUI()
        {
            m_PrepRole.OnClickAsObservable().Subscribe(_ =>
            {
                EventSystem.S.Send(EventID.OnTrainingSelectRole, m_PrepRoleModel);
            }).AddTo(this);

            m_ImgStateSub = m_PrepRoleModel.isSelected.Subscribe(val =>
            {
                m_State.gameObject.SetActive(val);
            }).AddTo(this);

            BindTraState();
        }
        #region IItemCom
        public void OnReset()
        {
            m_TrainStateSub?.Dispose();
            m_ImgStateSub?.Dispose();
            m_PrepRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
        }

        public void HandleSelectedRole(bool select, TrainingSlotModel trainingSlotModel = null)
        {
            m_PrepRoleModel.isSelected.Value = !m_PrepRoleModel.isSelected.Value;
            m_ImgStateSub?.Dispose();
            m_PrepRoleModel.trainingSlotModel = trainingSlotModel;
            if (select)
            {
                BindTraState();
            }
        }

        private void BindTraState()
        {
            m_ImgStateSub = m_PrepRoleModel.trainingSlotModel?.trainState.Subscribe(val =>
            {
                switch (val)
                {
                    case TrainingSlotState.Free:
                        m_PrepRole.enabled = true;
                        m_State.gameObject.SetActive(false);
                        break;
                    case TrainingSlotState.Training:
                        m_PrepRole.enabled = false;
                        m_State.gameObject.SetActive(true);
                        break;
                }
            }).AddTo(this);
        }
        #endregion
    }
}