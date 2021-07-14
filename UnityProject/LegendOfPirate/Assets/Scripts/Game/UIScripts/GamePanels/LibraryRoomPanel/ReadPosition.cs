using Qarth;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace GameWish.Game
{
    public class ReadPosition : UListItemView
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
        private LibrarySlotModel m_LibrarySlotModel;
        private IDisposable m_LibraryStateDis;
        private List<IDisposable> m_Cancelleds = new List<IDisposable>();
        #endregion

        #region Public
        public void OnRefresh(LibrarySlotModel librarySlotModel)
        {
            OnReset();

            m_LibrarySlotModel = librarySlotModel;

            BindModleToUI();
        }
        #endregion

        #region Private

        private void BindModleToUI()
        {
            m_LibraryStateDis = m_LibrarySlotModel.libraryState.Subscribe(val => HandlelibraryState(val)).AddTo(this) ;
        }

        private void HandlelibraryState(LibrarySlotState val)
        {
            ControlActiveChange(val);
            ClearAllDisciple();
            switch (val)
            {
                case LibrarySlotState.Free:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case LibrarySlotState.Reading:
                    IDisposable learnCountDownDis = m_LibrarySlotModel.readCountDown.SubscribeToTextMeshPro(m_Time).AddTo(this);
                    IDisposable timeProgressBarDis = m_LibrarySlotModel.timeProgressBar.Subscribe(bar=>HandleTimeBar(bar)).AddTo(this);
                  
                    m_Cancelleds.Add(learnCountDownDis);
                    m_Cancelleds.Add(timeProgressBarDis);
                    break;
                case LibrarySlotState.Locked:
                    IDisposable unlockLevelDis = m_LibrarySlotModel.unlockLevel.SubscribeToTextMeshPro(m_UnlockLevel).AddTo(this);
                    m_Cancelleds.Add(unlockLevelDis);
                    break;
                case LibrarySlotState.HeroSelected:
                    IDisposable heroIDDis =  m_LibrarySlotModel.heroID.Subscribe(id => HandleHeroID(id)).AddTo(this);
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

        private void ControlActiveChange(LibrarySlotState librarySlotState)
        {
            OnReset();
            switch (librarySlotState)
            {
                case LibrarySlotState.Free:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case LibrarySlotState.Reading:
                    m_RoleIconBg.gameObject.SetActive(true);
                    break;
                case LibrarySlotState.Locked:
                    m_LockBg.gameObject.SetActive(true);
                    break;
                case LibrarySlotState.HeroSelected:
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