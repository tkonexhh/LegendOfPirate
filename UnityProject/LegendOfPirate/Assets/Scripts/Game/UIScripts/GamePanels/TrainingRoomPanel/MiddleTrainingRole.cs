using Qarth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
    public enum TrainintRoomRoleState
    {
        /// <summary>
        /// Î´Ñ¡Ôñ
        /// </summary>
        NotSelected,
        /// <summary>
        /// ÑµÁ·ÖÐ
        /// </summary>
        Training,
        /// <summary>
        /// Î´½âËø
        /// </summary>
        NotUnlocked,
    }

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

        private int m_Index = -1;
        public void OnInit(int index)
        {
            m_Index = index;
        }
    }
	
}