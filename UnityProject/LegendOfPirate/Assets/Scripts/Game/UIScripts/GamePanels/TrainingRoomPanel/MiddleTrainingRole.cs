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
        private MiddleTrainingRoleModule m_MiddleTrainingRoleModule;
        #endregion
        #region Method
        private void OnReset()
        {
            m_RoleIconBg.gameObject.SetActive(false);
            m_Plug.gameObject.SetActive(false);
            m_LockBg.gameObject.SetActive(false);


        }
        private void OnRefresh()
        {
            switch (m_MiddleTrainingRoleModule.trainingSlotModel.trainState.Value)
            {
                case TrainintRoomRoleState.Freeing:
                    m_Plug.gameObject.SetActive(true);
                    break;
                case TrainintRoomRoleState.Training:
                    m_RoleIconBg.gameObject.SetActive(true);
                    break;
                case TrainintRoomRoleState.NotUnlocked:
                    m_LockBg.gameObject.SetActive(true);
                    break;
            }
        }

        public void OnInit(MiddleTrainingRoleModule middleTrainingRoleModule)
        {
            OnReset();

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