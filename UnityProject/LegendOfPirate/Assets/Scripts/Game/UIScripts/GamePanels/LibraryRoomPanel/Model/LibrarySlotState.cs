using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public enum LibrarySlotState
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 训练中
        /// </summary>
        Reading = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        HeroSelected = 3,
    }
}