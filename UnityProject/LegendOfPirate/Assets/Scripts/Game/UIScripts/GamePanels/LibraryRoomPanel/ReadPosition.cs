using Qarth;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;

namespace GameWish.Game
{
    public class ReadPosition : UListItemView
    {
        #region SerializeField
        [SerializeField]
        private Image m_Plug;
        [SerializeField]
        private Image m_RoleIconBg;
        [SerializeField]
        private Image m_RoleIcon;
        [SerializeField]
        private TextMeshProUGUI m_Time;
        [SerializeField]
        private Image m_LockBg;
        [SerializeField]
        private Image m_Lock;
        #endregion

        #region Data
        private ReadPostionModel m_LibPosModel;

        private IDisposable m_LibtateDis;
        #endregion

        #region Other Method
        public void OnRefresh(ReadPostionModel middle)
        {
            OnReset();

            if (middle == null)
            {
                Debug.LogWarning("middleLibraryRoleModule is null");
                return;
            }

            m_LibPosModel = middle;

            BindModleToUI();

        }

        private void BindModleToUI()
        {
            m_LibPosModel.librarySlotModel.libraryRemainTime.Select(x => (int)x).SubscribeToTextMeshPro(m_Time).AddTo(this);

            m_LibtateDis = m_LibPosModel.librarySlotModel?.libraryState.Subscribe(val=> {
                OnReset();
                switch (val)
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
                }
            }).AddTo(this);
        }

        private void OnReset()
        {
            //m_LibtateDis?.Dispose();
            m_Plug.gameObject.SetActive(false);
            m_RoleIconBg.gameObject.SetActive(false);
            m_LockBg.gameObject.SetActive(false);
        }
        #endregion
    }
}