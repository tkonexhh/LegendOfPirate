using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public enum LibrarySlotState
    {
        /// <summary>
        /// ������
        /// </summary>
        Free = 0,
        /// <summary>
        /// ѵ����
        /// </summary>
        Reading = 1,
        /// <summary>
        /// δ����
        /// </summary>
        Locked = 2,
        /// <summary>
        /// ѡ����δ��ʼ
        /// </summary>
        HeroSelected = 3,
    }
}