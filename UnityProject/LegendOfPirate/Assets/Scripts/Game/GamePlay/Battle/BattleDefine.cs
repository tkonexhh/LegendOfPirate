using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    /// 伤害范围
    /// </summary>
    public enum DamageRangeEnum
    {
        Circle,//圆形
        Sector,//扇形
        Rect,//矩形
    }

    /// <summary>
    /// 战斗方式
    /// </summary>
    public enum AttackTypeEnum
    {
        Lock,
        Range,
        Shoot,
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
    public enum SensorTypeEnum
    {
        HP,
        HPRate,
        Nearest,
    }
}