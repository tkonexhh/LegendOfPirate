using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BuffDefine
    {

    }



    [LabelText("属性类型")]
    public enum AttributeType
    {
        [LabelText("空")] None,
        [LabelText("生命值")] Hp,
        [LabelText("最大生命值%")] MaxHp,
        [LabelText("攻击力%")] ATK,
        [LabelText("移动速度%")] MoveSpeed,
        [LabelText("攻击间隔%")] AttackRate,
        [LabelText("护甲%")] Amor,
        [LabelText("暴击")] Critical,
        [LabelText("额外生命值(最大生命值%)")] ExtraHp,
        [LabelText("闪避%")] EvasionRate,
        [LabelText("吸血%")] AtkHealRate,

    }

    [LabelText("Buff持续时间叠加类型(完成)")]
    public enum BuffAppendType
    {
        [LabelText("刷新")] Reset,
        [LabelText("累加")] Additive,
        [LabelText("继续")] Continue,
    }

    [LabelText("触发方式")]
    public enum BuffTriggerType
    {
        [LabelText("创建时")] Create,
        [LabelText("攻击时")] Attack,
        [LabelText("受击时触发")] Hurt,
        [LabelText("移动触发")] Move,
        [LabelText("间隔触发")] Interval,
    }

    [LabelText("状态类型(完成)")]
    [Flags]
    public enum StatusControlType
    {
        [LabelText("（空）")] None = 0,
        [LabelText("移动禁止")] MoveForbid = 1 << 0,
        [LabelText("施法禁止")] SkillForbid = 1 << 1,
        [LabelText("攻击禁止")] AttackForbid = 1 << 2,
        [LabelText("免疫伤害")] HurtForbid = 1 << 3,

    }

}