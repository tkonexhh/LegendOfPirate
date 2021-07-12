using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public enum TrainingSlotState
    {
        /// <summary>
        /// ������
        /// </summary>
        Free = 0,
        /// <summary>
        /// ѵ����
        /// </summary>
        Training = 1,
        /// <summary>
        /// δ����
        /// </summary>
        Locked = 2,
    }
}