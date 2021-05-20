using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public enum EventID
    {
        OnLanguageTableSwitchFinish,
        OnAddCoinNum,
        OnAddDiamondNum,
        OnUpdateLoadProgress,
        OnShowPopAdUI,
        OnStartAdEffect,
        OnEndAdEffect,
        OnLevelPassed,
        OnTimeRefresh,
        OnGuidePanelOpen,
        GuideDelayStart,
        GuideEventTrigger,
        OnAddTipCount,
        OnAddStarCount,
        OnWatchAd,

        /// <summary>
        /// 收集完目标
        /// </summary>
        OnCollectedTarget,
        OnTimeOut,//超时
        OnGameSuccess,
        OnGameStart,
        OnTipCountChange,
    }

}
