using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public class AdEffectParams
	{
        /// <summary>
        /// 是否看广告增加了我方士兵的HP
        /// </summary>
        public static bool isInHPAdEnhance = false;

        /// <summary>
        /// 是否看广告增加了我方士兵手动召唤数量
        /// </summary>
        public static bool isInSummonCountAdEnhance = false;

        /// <summary>
        /// 是否看广告增加了我方士兵自动召唤速度
        /// </summary>
        public static bool isInSpawnSpeedAdEnhance = false;

        /// <summary>
        /// 是否看广告增加了我方在线双倍收益
        /// </summary>
        public static bool isInDoubleIncomeAdEnhance = false;

        /// <summary>
        /// 是否看广告增加了我方离线双倍收益
        /// </summary>
        public static bool isInOfflineDoubleIncomeAdEnhance = false;

        public static float GetADHPRatio()
        {
            if (isInHPAdEnhance)
            {
                return 1.2f;
            }

            return 1f;
        }
	}	
}