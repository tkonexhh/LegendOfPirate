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
        /// <summary>
        /// 升级刷新事件
        /// </summary>
        OnTrainingUpgradeRefresh,
        /// <summary>
        /// 开始训练
        /// </summary>
        OnTRoomStartTraining,
        #endregion
    }

}
