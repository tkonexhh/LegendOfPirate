using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace GameWish.Game
{
    public class BottomTrainingRole : UListItemView
    {
        [SerializeField]
        private Button m_BottomTrainingRole;
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private Image m_State;

        #region Data
        private BottomTrainingRoleModule m_BottomTrainingRoleData;
        private IntReactiveProperty m_IntReactiveIndex;
        #endregion

        private void Awake()
        {
        }

    


        public void OnInit(BottomTrainingRoleModule bottomTrainingRoleData, IntReactiveProperty intReactiveProperty)
        {
            OnReset();
            if (bottomTrainingRoleData == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }

            m_BottomTrainingRoleData = bottomTrainingRoleData;
            m_IntReactiveIndex = intReactiveProperty;

            OnRefresh();
        }
        #region IItemCom
        public void OnReset()
        {
            m_BottomTrainingRole.onClick.RemoveAllListeners();
            m_State.gameObject.SetActive(false);
            m_BottomTrainingRole.OnClickAsObservable().Subscribe(_ =>
            {
                m_BottomTrainingRoleData.isSelected.Value = !m_BottomTrainingRoleData.isSelected.Value;
                //m_BottomTrainingRoleData.bottomTrainingRoleType.Value = (m_BottomTrainingRoleData.bottomTrainingRoleType.Value. == BottomTrainingRoleType.NotSelected ? BottomTrainingRoleType.Selected : BottomTrainingRoleType.NotSelected);
                m_IntReactiveIndex.Value += (m_BottomTrainingRoleData.isSelected.Value ? 1 : -1);
                //EventSystem.S.Send(EventID.OnBottomTrainingRole, );
                OnRefresh();
            });
        }

        public void OnRefresh()
        {
            if (m_BottomTrainingRoleData.isSelected.Value)
            {
                m_State.gameObject.SetActive(true);
            }
            else
            {
                m_State.gameObject.SetActive(false);
            }
        }
        #endregion
    }

}