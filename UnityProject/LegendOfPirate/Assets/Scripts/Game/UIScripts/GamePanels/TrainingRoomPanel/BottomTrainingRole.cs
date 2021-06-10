using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace GameWish.Game
{
    public enum BottomTrainingRoleType
    {
        Selected,
        NotSelected,
    }

    public class BottomTrainingRole : UListItemView
    {
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;

        #region Data
        private int m_Index = -1;
        private BottomTrainingRoleType m_BottomTrainingRoleType = BottomTrainingRoleType.NotSelected;
        #endregion

        private void Awake()
        {
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                m_BottomTrainingRoleType = m_BottomTrainingRoleType == BottomTrainingRoleType.NotSelected ? BottomTrainingRoleType.Selected : BottomTrainingRoleType.NotSelected;
                EventSystem.S.Send(EventID.OnBottomTrainingRole, m_BottomTrainingRoleType == BottomTrainingRoleType.NotSelected ? -1 : 1);
                RefreshBottomTrainingRole();
            });
        }

        private void RefreshBottomTrainingRole()
        {
            switch (m_BottomTrainingRoleType)
            {
                case BottomTrainingRoleType.Selected:
                    m_State.gameObject.SetActive(true);
                    break;
                case BottomTrainingRoleType.NotSelected:
                    m_State.gameObject.SetActive(false);
                    break;
            }
        }


        public void OnInit(int index)
        {
            m_Index = index;
            RefreshBottomTrainingRole();
        }
    }

}