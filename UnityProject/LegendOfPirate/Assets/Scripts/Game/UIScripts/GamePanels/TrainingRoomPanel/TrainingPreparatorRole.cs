using Qarth;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameWish.Game
{
    public class TrainingPreparatorRole : UListItemView
    {
        #region SerializeField
        [SerializeField] private Button m_TrainingRoleBtn;
        [SerializeField] private Image m_Icon;
        #endregion
        #region Data
        private TrainingPreparatorRoleModel m_BottomTrainingData;

        private IDisposable m_BindSlotDis;
        private List<IDisposable> m_DisCache = new List<IDisposable>();
        #endregion
        public void OnRefresh(TrainingPreparatorRoleModel bottom)
        {
            OnReset();

            m_BottomTrainingData = bottom;

            BindModelToUI();

            BindUIToModel();
        }

        private void BindUIToModel()
        {
            m_TrainingRoleBtn.OnClickAsObservable().Subscribe(_ => { m_BottomTrainingData.OnClickRoleModel(); }).AddTo(this);
        }

        private void BindModelToUI()
        {
            m_BindSlotDis = m_BottomTrainingData.IsBindSlot.Subscribe(val => HandleBindSlot(val)).AddTo(this);
        }

        private void HandleBindSlot(bool val)
        {
            if (val)
            {
                IDisposable TrainingStateDis = m_BottomTrainingData.TrainingSlotModel.trainingState.Subscribe(state => HandleTrainingState(state)).AddTo(this);
                m_DisCache.Add(TrainingStateDis);
            }
            else
            {
                ClearCache();
            }
        }

        private void HandleTrainingState(TrainingSlotState state)
        {
            switch (state)
            {
                case TrainingSlotState.Free:
                    m_TrainingRoleBtn.interactable = true;
                    Narrow();
                    break;
                case TrainingSlotState.Training:
                    m_TrainingRoleBtn.interactable = false;
                    break;
                case TrainingSlotState.Locked:
                    break;
                case TrainingSlotState.HeroSelected:
                    Enlarge();
                    break;
            }
        }

        private void Enlarge()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(110, 110);//¡Ÿ ±

            //m_PrepRoleModel.TrainingRoomPanel.AdjustViewportSize();
        }

        private void Narrow()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);//¡Ÿ ±
        }

        private void ClearCache()
        {
            foreach (var item in m_DisCache)
                item.Dispose();
        }

        private void OnDestroy()
        {
            m_BottomTrainingData.ClearRoleSelect();
        }

        private void OnReset()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);//¡Ÿ ±
            m_TrainingRoleBtn.interactable = true;
            m_BindSlotDis?.Dispose();
            m_TrainingRoleBtn.onClick.RemoveAllListeners();
        }
    }
}