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

    [LabelText("被动技能触发类型(完成)")]
    public enum PassiveSkillTriggerType
    {
        [LabelText("攻击时触发")] Attack,
        [LabelText("受击时触发")] Hurt,
        [LabelText("时间间隔触发")] Time,
        [LabelText("移动触发")] Move,
        [LabelText("立即触发")] Forver,
    }

    [LabelText("属性类型")]
    public enum AttributeType
    {
        [LabelText("空")] None,
        [LabelText("生命值")] Hp,
        [LabelText("最大生命值%(完成)")] MaxHp,
        [LabelText("攻击力(完成)")] ATK,
        [LabelText("移动速度%(完成)")] MoveSpeed,
        [LabelText("攻击间隔")] AttackRate,
        [LabelText("护甲")] Amor,
        [LabelText("暴击")] Critical,
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
        [LabelText("（空）")] None = 0,
        [LabelText("创建时")] Create,
        [LabelText("攻击时")] Attacked,
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

    }

}