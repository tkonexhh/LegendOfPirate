using Qarth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace GameWish.Game
{
    public class TrainingPosition : UListItemView
    {
        #region SerializeField
        [SerializeField, Header("Unselected")] private Image m_Plug;
        [SerializeField, Header("Selected")] private Image m_RoleIconBg;
        [SerializeField] private Image m_RoleIcon;
        [SerializeField] private TextMeshProUGUI m_Time;
        [SerializeField] private Image m_TimeBarBg;
        [SerializeField] private Image m_TimeBar;
        [SerializeField] private Image m_RedPoint;
        [SerializeField, Header("Lock")] private Image m_LockBg;
        [SerializeField] private Image m_Lock;
        [SerializeField] private TextMeshProUGUI m_UnlockLevel;
        #endregion
        #region Data
        private TrainingPositionModel m_TraPosModel;
        #endregion
        #region Method
        private void OnReset()
        {
            m_RoleIconBg.gameObject.SetActive(false);
            m_Plug.gameObject.SetActive(false);
            m_LockBg.gameObject.SetActive(false);
        }
        #region Public
        public void OnRefresh(TrainingPositionModel traPosModel)
        {
            OnReset();
            if (traPosModel == null)
            {
                Debug.LogWarning("traPosModel is null");
                return;
            }

            m_TraPosModel = traPosModel;

            BindModelToUI();
        }
        #endregion
        #region Private
        private void BindModelToUI()
        {
            m_TraPosModel.unlockLevel.Select(val => LanguageKeyDefine.FIXED_TITLE_LV_Ⅰ + val).SubscribeToTextMeshPro(m_UnlockLevel).AddTo(this);
            m_TraPosModel.trainingCountDown.SubscribeToTextMeshPro(m_Time).AddTo(this);
            m_TraPosModel.progressBar.Subscribe(ValueTuple => HandleTimeBar(ValueTuple)).AddTo(this);
            m_TraPosModel.isHaveRole.SubscribeToActive(m_RoleIconBg, m_Plug).ForEach(i => i.AddTo(this));
            m_TraPosModel.GetTrainingSlotState().Subscribe(val => { HandleTrainingSlotState(val); }).AddTo(this);
        }

        private void HandleTimeBar(float valueTuple)
        {
            m_TimeBar.fillAmount = valueTuple;
        }

        private void HandleTrainingSlotState(TrainingSlotState val)
        {
            OnReset();
            switch (val)
            {
                case TrainingSlotState.Free:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.Training:
                    m_RoleIconBg.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.Locked:
                    m_LockBg.gameObject.SetActive(true);
                    break;
            }
        }
        #endregion
        #endregion
    }
}