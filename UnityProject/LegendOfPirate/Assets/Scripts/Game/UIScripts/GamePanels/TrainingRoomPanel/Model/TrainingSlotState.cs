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
        /// ø’œ–÷–
        /// </summary>
        Free = 0,
        /// <summary>
        /// —µ¡∑÷–
        /// </summary>
        Training = 1,
        /// <summary>
        /// Œ¥Ω‚À¯
        /// </summary>
        Locked = 2,
    }
}