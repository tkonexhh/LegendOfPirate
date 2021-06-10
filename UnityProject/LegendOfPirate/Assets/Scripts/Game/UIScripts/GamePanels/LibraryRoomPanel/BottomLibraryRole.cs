using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class BottomLibraryRole : UListItemView
	{
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;
        #region Data
        private BottomLibraryRoleModule m_BottomLibraryData;
        private IntReactiveProperty m_IntReactiveIndex;

        #endregion
        public void OnInit(BottomLibraryRoleModule bottomLibraryRoleModule, IntReactiveProperty intReactiveProperty)
        {
            ResetState();
            if (bottomLibraryRoleModule == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }
            m_BottomLibraryData = bottomLibraryRoleModule;
            m_IntReactiveIndex = intReactiveProperty;

            RefreshBottomTrainingRole();
        }
        private void RefreshBottomTrainingRole()
        {
            if (m_BottomLibraryData.isSelected.Value)
            {
                m_State.gameObject.SetActive(true);
            }
            else
            {
                m_State.gameObject.SetActive(false);
            }
        }
        private void ResetState()
        {
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                m_BottomLibraryData.isSelected.Value = !m_BottomLibraryData.isSelected.Value;
                m_IntReactiveIndex.Value += (m_BottomLibraryData.isSelected.Value ? 1 : -1);
                RefreshBottomTrainingRole();
            });
        }
    }
	
}