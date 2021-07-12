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
        [SerializeField]
        private Button m_PrepRole;
        [SerializeField]
        private Image m_Icon;

        #region Data
        private TrainingPreparatorRoleModel m_PrepRoleModel;
        private List<IDisposable> m_Disposables = new List<IDisposable>();
        private Vector2 m_DefaultSizeDelta;
        private RectTransform m_Rect;
        #endregion

        public void OnRefresh(TrainingPreparatorRoleModel traPrepRoleModel)
        {
            OnReset();

            m_PrepRoleModel = traPrepRoleModel;

            BindModelToUI();
        }
        private void BindModelToUI()
        {
            IDisposable prepRole = m_PrepRole.OnClickAsObservable().Subscribe(_ => { HandleSelectedRole(); }).AddTo(this);

            m_Disposables.Add(prepRole);
        }
        #region Public
        public void OnReset()
        {
            m_Rect = transform as RectTransform;
            m_DefaultSizeDelta = new Vector2(100,100);

            foreach (var item in m_Disposables)
                item.Dispose();
            m_Disposables.Clear();
        }

        public void HandleSelectedRole()
        {
            if (m_PrepRoleModel.IsEmpty.Value)
            {
                TrainingSlotModel trainingSlotModel = m_PrepRoleModel.TrainingRoomModel.GetFreeSlot();
                if (trainingSlotModel != null)
                {
                    Enlarge();
                    m_PrepRoleModel.BindSoltAndRole(trainingSlotModel);
                    trainingSlotModel.SetTemporaryRoleID(m_PrepRoleModel.GetRoleID());
                    m_PrepRoleModel.TrainingRoomPanel.AddSelectedCount();
                }
                else
                    FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.TRAININGROOM_CONT_1);
            }
            else
            {
                Narrow();
                m_PrepRoleModel.TrainingSlotModel.ClearTemporaryRoleID();
                m_PrepRoleModel.BindSoltAndRole();
                m_PrepRoleModel.TrainingRoomPanel.ReduceSelectedCount();
            }
        }
        #endregion

        #region Private
        private void Enlarge()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(110, 110);//¡Ÿ ±

            //m_PrepRoleModel.TrainingRoomPanel.AdjustViewportSize();
        }

        private void Narrow()
        {
            RectTransform rect = transform as RectTransform;
            rect.sizeDelta = new Vector2(100,100);//¡Ÿ ±
        }

        private void OnDestroy()
        {
            if (!m_PrepRoleModel.IsEmpty.Value)
            {
                m_PrepRoleModel.TrainingSlotModel.ClearTemporaryRoleID();
                m_PrepRoleModel.BindSoltAndRole();
            }
        }
        #endregion
    }
}