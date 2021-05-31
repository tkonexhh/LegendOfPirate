using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class SkillDefine
    {

    }

    [LabelText("属性类型")]
    public enum AttributeType
    {
        [LabelText("空")] None,
        [LabelText("生命值")] Hp,
        [LabelText("最大生命值")] MaxHp,
        [LabelText("攻击力")] AP,
        [LabelText("移动速度")] MoveSpeed,
        [LabelText("攻击间隔")] AttackRate,
        [LabelText("护甲")] Amor,
        [LabelText("暴击")] Critical,
    }

    [LabelText("修饰类型")]
    public enum ModifyType
    {
        [LabelText("数值")] Add,
        [LabelText("百分比")] PercentAdd,
    }


    [LabelText("技能类型")]
    public enum SkillType
    {
        [LabelText("主动技能")] Initiative,
        [LabelText("被动技能")] Passive,
    }

    [LabelText("释放对象")]
    public enum PickTargetType
    {
        [LabelText("自身")] Self,
        [LabelText("己方")] Our,
        [LabelText("敌方")] Enemy,
        [LabelText("当前目标")] Target,
    }

    [LabelText("被动技能触发类型")]
    public enum PassiveSkillTriggerType
    {
        [LabelText("攻击时触发")] Attack,
        [LabelText("受击时触发")] Hurt,
        [LabelText("时间间隔触发")] Time,
        [LabelText("移动触发")] Move,
        [LabelText("永久触发")] Forver,
    }

}