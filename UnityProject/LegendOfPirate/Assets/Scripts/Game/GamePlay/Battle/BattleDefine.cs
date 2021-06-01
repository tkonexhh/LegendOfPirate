using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BattleDefine
    {

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

    [LabelText("伤害方式")]
    public enum DamageType
    {
        [LabelText("单体")] Single,
        [LabelText("范围")] Range,
    }


    /// <summary>
    /// 伤害范围
    /// </summary>
    [LabelText("伤害范围")]
    public enum DamageRangeType
    {
        [LabelText("圆形")] Circle,
        [LabelText("扇形")] Sector,
        [LabelText("矩形")] Rect,
    }

    /// <summary>
    /// 人物类型
    /// </summary>
    public enum RoleTypeEnum
    {
        Warrior,
        Wizard,
        Assassin,
    }

    /// <summary>
    /// 索敌类型
    /// </summary>
    [LabelText("索敌类型")]
    public enum SensorTypeEnum
    {
        [LabelText("血量最低")] HPLow,
        [LabelText("血量最高")] HPHigh,
        [LabelText("血量百分比最低")] HPRateLow,
        [LabelText("血量百分比最高")] HPRateHigh,
        [LabelText("最近")] Nearest,
        [LabelText("最远")] Farest,
    }
}