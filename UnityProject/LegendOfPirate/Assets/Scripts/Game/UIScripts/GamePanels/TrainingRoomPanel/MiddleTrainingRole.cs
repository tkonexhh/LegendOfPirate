using Qarth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class MiddleTrainingRole : UListItemView
	{
        [SerializeField]
        private Image m_Plug;
        [SerializeField]
        private Image m_RoleIconBg;
        [SerializeField]
        private Image m_RoleIcon;
        [SerializeField]
        private TextMeshProUGUI m_Time;
        [SerializeField]
        private Image m_TimeBarBg;
        [SerializeField]
        private Image m_TimeBar;
        [SerializeField]
        private Image m_LockBg;
        [SerializeField]
        private Image m_Lock;

        #region Data
        private MiddleSlotModel m_MiddleTrainingRoleModule;
        #endregion
        #region Method
        private void OnReset()
        {
            m_RoleIconBg.gameObject.SetActive(false);
            m_Plug.gameObject.SetActive(false);
            m_LockBg.gameObject.SetActive(false);


        }
        public void OnRefresh()
        {
            OnReset();
            switch (m_MiddleTrainingRoleModule.trainingSlotModel.trainState.Value)
            {
                case TrainingSlotState.Free:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.Training:
                    m_RoleIconBg.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.Locked:
                    m_LockBg.gameObject.SetActive(true);
                    break;
                case TrainingSlotState.SelectedNotStart:
                    m_RoleIconBg.gameObject.SetActive(true);
                    m_Time.text = "选择";
                    break;
            }
        }

        public void OnInit(MiddleSlotModel middleTrainingRoleModule)
        {
            if (middleTrainingRoleModule == null)
            {
                Debug.LogWarning("bottomTrainingRoleData is null");
                return;
            }

            m_MiddleTrainingRoleModule = middleTrainingRoleModule;

            OnRefresh();
        }
        #endregion
    }

}