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
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;
        #region Data
        private LibraryPreparatorRoleModel m_BottomLibraryData;

        private IDisposable m_LibSlotState;
        #endregion
        public void OnInit(LibraryPreparatorRoleModel bottom)
        {
            OnReset();
            if (bottom == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }
            m_BottomLibraryData = bottom;

            BindModelToUI();

            OnClickAddListener();

            OnRefresh();
        }
        private void OnClickAddListener()
        {
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                EventSystem.S.Send(EventID.OnLibrarySelectRole, m_BottomLibraryData);
            }).AddTo(this);
        }
        private void BindModelToUI()
        {
            m_LibSlotState = m_BottomLibraryData.librarySlotModel?.libraryState.Subscribe(val => {
                switch (val)
                {
                    case LibrarySlotState.Free:
                        m_BottomTrainingRole.enabled = true;
                        m_State.enabled = false;
                        break;
                    case LibrarySlotState.Reading:
                        m_BottomTrainingRole.enabled = false;
                        m_State.enabled = true;
                        break;
                    case LibrarySlotState.Locked:
                        m_BottomTrainingRole.enabled = false;
                        m_State.enabled = false;
                        break;
                    case LibrarySlotState.HeroSelected:
                        m_BottomTrainingRole.enabled = true;
                        m_State.enabled = true;
                        break;
                }
            }).AddTo(this);
        }
        public void HandleSelectedRole(bool select, LibrarySlotModel librarySlotModel = null)
        {
            m_BottomLibraryData.librarySlotModel = librarySlotModel;
            m_LibSlotState?.Dispose();
            if (select)
            {
                BindModelToUI();
            }
        }

        private void OnRefresh()
        {
         
        }
        private void OnReset()
        {
            m_LibSlotState?.Dispose();
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.enabled = false;
        }
    }
}