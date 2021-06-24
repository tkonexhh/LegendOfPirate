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


        #region RandomDefenseChoose
        /// <summary>
        /// 选择弟子事件
        /// </summary>
        OnRandomDefenseChooseSelectedRole,
        #endregion

        #region TrainingRoom
        /// <summary>
        /// 选择角色
        /// </summary>
        OnTrainingSelectRole,
        #endregion

        #region Library
        /// <summary>
        /// 选择角色
        /// </summary>
        OnLibrarySelectRole,
        #endregion

        #region Task
        /// <summary>
        /// 日常任务完成加1
        /// </summary>
        DailyTimesAdd,
        /// <summary>
        /// 成就任务完成加1
        /// </summary>
        AchievementTimesAdd,
        /// <summary>
        /// 主线任务完成加1
        /// </summary>
        MainTaskTimesAdd,
        /// <summary>
        /// 日常任务刷新
        /// </summary>
        DailyTaskRefresh,
        /// <summary>
        /// 成就任务刷新
        /// </summary>
        AchievementTaskRefresh,
        /// <summary>
        /// 主线任务刷新
        /// </summary>
        MainTaskRefresh,
        #endregion
    }

}
