using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public enum ForgeStage
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 锻造中
        /// </summary>
        Forging = 1,
        ///<summary>
        /// 未解锁
        ///</summary>
        Lock = 2,
        /// <summary>
        /// 选中
        /// </summary>
        Selected = 3,
    }
}