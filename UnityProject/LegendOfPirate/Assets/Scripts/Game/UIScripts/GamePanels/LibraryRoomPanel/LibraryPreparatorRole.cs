using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
    public class LibraryPreparatorRole : UListItemView
    {
        #region SerializeField
        [SerializeField] private Button m_LibraryRole;
        [SerializeField] private Image m_Icon;
        #endregion
        #region Data
        private LibraryPreparatorRoleModel m_BottomLibraryData;

        private IDisposable m_BindSlotDis;
        private List<IDisposable> m_DisCache = new List<IDisposable>();
        #endregion
        public void OnRefresh(LibraryPreparatorRoleModel bottom)
        {
            OnReset();

            m_BottomLibraryData = bottom;

            BindModelToUI();

            BindUIToModel();
        }

        private void BindUIToModel()
        {
            m_LibraryRole.OnClickAsObservable().Subscribe(_ => { m_BottomLibraryData.OnClickRoleModel(); }).AddTo(this);
        }

        private void BindModelToUI()
        {
            m_BindSlotDis = m_BottomLibraryData.IsBindSlot.Subscribe(val => HandleBindSlot(val)).AddTo(this);
        }

        private void HandleBindSlot(bool val)
        {
            if (val)
            {
                IDisposable libraryStateDis = m_BottomLibraryData.LibrarySlotModel.libraryState.Subscribe(state => HandleLibraryState(state)).AddTo(this);
                m_DisCache.Add(libraryStateDis);
            }
            else
            {
                ClearCache();
            }
        }

        private void HandleLibraryState(LibrarySlotState state)
        {
            switch (state)
            {
                case LibrarySlotState.Free:
                    m_LibraryRole.interactable = true;
                    Narrow();
                    break;
                case LibrarySlotState.Reading:
                    m_LibraryRole.interactable = false;
                    break;
                case LibrarySlotState.Locked:
                    break;
                case LibrarySlotState.HeroSelected:
                    Enlarge();
                    break;
            }
        }

        private void Enlarge()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(110, 110);//��ʱ

            //m_PrepRoleModel.TrainingRoomPanel.AdjustViewportSize();
        }

        private void Narrow()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);//��ʱ
        }

        private void ClearCache()
        {
            foreach (var item in m_DisCache)
                item.Dispose();
        }

        private void OnDestroy()
        {
            m_BottomLibraryData.ClearRoleSelect();
        }

        private void OnReset()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100, 100);//��ʱ
            m_LibraryRole.interactable = true;
            m_BindSlotDis?.Dispose();
            m_LibraryRole.onClick.RemoveAllListeners();
        }
    }
}