using Qarth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections.Generic;

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
        private TrainingSlotModel m_TrainingSlotModel;
        private IDisposable m_TrainingStateDis;
        private List<IDisposable> m_Cancelleds = new List<IDisposable>();
        #endregion

        #region Public
        public void OnRefresh(TrainingSlotModel TrainingSlotModel)
        {
            OnReset();

            m_TrainingSlotModel = TrainingSlotModel;

            BindModleToUI();
        }
        #endregion

        #region Private

        private void BindModleToUI()
        {
            m_TrainingStateDis = m_TrainingSlotModel.trainingState.Subscribe(val => HandleTrainingState(val)).AddTo(this);
        }

        private void HandleTrainingState(TrainingSlotState val)
        {
            ControlActiveChange(val);
            ClearAllDisciple();
            switch (val)
            {
                case TrainingSlotState.Free:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.Training:
                    IDisposable learnCountDownDis = m_TrainingSlotModel.trainingCountDown.SubscribeToTextMeshPro(m_Time).AddTo(this);
                    IDisposable timeProgressBarDis = m_TrainingSlotModel.timeProgressBar.Subscribe(bar => HandleTimeBar(bar)).AddTo(this);

                    m_Cancelleds.Add(learnCountDownDis);
                    m_Cancelleds.Add(timeProgressBarDis);
                    break;
                case TrainingSlotState.Locked:
                    IDisposable unlockLevelDis = m_TrainingSlotModel.unlockLevel.SubscribeToTextMeshPro(m_UnlockLevel).AddTo(this);
                    m_Cancelleds.Add(unlockLevelDis);
                    break;
                case TrainingSlotState.HeroSelected:
                    IDisposable heroIDDis = m_TrainingSlotModel.heroID.Subscribe(id => HandleHeroID(id)).AddTo(this);
                    m_Cancelleds.Add(heroIDDis);
                    break;
                default:
                    break;
            }
        }

        private void HandleTimeBar(float val)
        {
            m_TimeBar.fillAmount = val;
        }

        private void HandleHeroID(int val)
        {
            //TODO 根据ID加载头像
            if (val == -1)
            {
                m_RoleIcon.gameObject.SetActive(false);
            }
            else
            {
                m_RoleIcon.gameObject.SetActive(true);
            }
        }

        private void ControlActiveChange(TrainingSlotState trainingSlotState)
        {
            OnReset();
            switch (trainingSlotState)
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
                case TrainingSlotState.HeroSelected:
                    m_RoleIconBg.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void OnReset()
        {
            m_RoleIcon.gameObject.SetActive(true);
            m_RedPoint.gameObject.SetActive(false);
            m_Plug.gameObject.SetActive(false);
            m_RoleIconBg.gameObject.SetActive(false);
            m_LockBg.gameObject.SetActive(false);
            ClearAllDisciple();
        }

        private void ClearAllDisciple()
        {
            foreach (var item in m_Cancelleds)
                item.Dispose();
        }
        #endregion
    }
}