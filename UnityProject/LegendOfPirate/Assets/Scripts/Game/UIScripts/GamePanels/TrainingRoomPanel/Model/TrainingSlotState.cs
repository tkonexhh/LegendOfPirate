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
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 训练中
        /// </summary>
        Training = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选中状态
        /// </summary>
        Select = 3,
    }
}