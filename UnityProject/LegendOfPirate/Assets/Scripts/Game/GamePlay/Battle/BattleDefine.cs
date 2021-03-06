using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BattleDefine
    {
        public const string ROLEANIM_RUN = "run";
        public const string ROLEANIM_IDLE = "idle";
        public const string ROLEANIM_DEATH = "death";
        public const string ROLEANIM_ATTACK01 = "attack01";
        public const string ROLEANIM_SKILL01 = "skill001";
        public const string ROLEANIM_SKILL02 = "skill002";
        public const string ROLEANIM_VICTORY = "victory";

        public const int BATTLE_WIDTH = 4;
        public const int BATTLE_HEIGHT = 3;
        public const float BATTLE_CELL_WIDTH = 5;
        public const float BATTLE_CELL_HEIGHT = 5;
        public const float BATTLE_HALF_CENTERHEIGHT = 15;

        public const string POOLNAME_BATTLEFIELD = "BattleField";
    }

    /// <summary>
    /// 阵营
    /// </summary>
    public enum BattleCamp
    {
        Our,
        Enemy,
    }

    /// <summary>
    /// 战斗方式
    /// </summary>
    [LabelText("战斗方式")]
    public enum AttackType
    {
        [LabelText("锁定攻击")] Lock,
        [LabelText("远程攻击")] Shoot,
    }


    /// <summary>
    /// 伤害范围
    /// </summary>
    [LabelText("伤害范围")]
    public enum DamageRangeType
    {
        [LabelText("单体")] Single,
        [LabelText("范围")] Range,
    }

    /// <summary>
    /// 索敌类型
    /// </summary>
    [LabelText("索敌类型")]
    public enum SensorTypeEnum
    {
        [LabelText("最近")] Nearest,
        [LabelText("最远")] Farest,
        [LabelText("血量最低")] HPLow,
        [LabelText("血量最高")] HPHigh,
        [LabelText("血量百分比最低")] HPRateLow,
        [LabelText("血量百分比最高")] HPRateHigh,
    }
}