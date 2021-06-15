using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
    public class BottomLaboratoryPotion : UListItemView
    {
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;

        #region Data
        private BottomLaboratoryPotionModule m_BottomLaboratoryPotionModule;
        #endregion


        public void OnInit(BottomLaboratoryPotionModule bottomLaboratoryPotionModule)
        {
            OnReset();
            m_BottomLaboratoryPotionModule = bottomLaboratoryPotionModule;
            OnRefresh();
        }

        public void OnRefresh()
        {
            if (m_BottomLaboratoryPotionModule.isSelected.Value)
            {
                m_State.gameObject.SetActive(true);
            }
            else
            {
                m_State.gameObject.SetActive(false);
            }
        }

        public void OnReset()
        {
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                m_BottomLaboratoryPotionModule.isSelected.Value = !m_BottomLaboratoryPotionModule.isSelected.Value;
                OnRefresh();
            });
        }

    }

}