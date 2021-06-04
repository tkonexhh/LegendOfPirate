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


    /// <summary>
    /// 伤害范围
    /// </summary>
    [LabelText("伤害范围")]
    [PropertyTooltip("必须以;来分隔\n参数说明:\n单体:无需参数\n圆形:参数1:半径\n扇形:参数1:半径,参数2:角度\n矩形:参数1:宽度,参数2:高度")]
    public enum DamageRangeType
    {
        [LabelText("单体")] Single,
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
        [LabelText("血量最低(完成)")] HPLow,
        [LabelText("血量最高(完成)")] HPHigh,
        [LabelText("血量百分比最低")] HPRateLow,
        [LabelText("血量百分比最高")] HPRateHigh,
        [LabelText("最近(完成)")] Nearest,
        [LabelText("最远")] Farest,
    }
}