using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class SkillDefine
    {

    }




    [LabelText("技能类型")]
    public enum SkillType
    {
        [LabelText("主动技能")] Initiative,
        [LabelText("被动技能")] Passive,
    }

    [LabelText("释放对象(完成)")]
    public enum PickTargetType
    {
        [LabelText("自身")] Self,
        [LabelText("己方")] Our,
        [LabelText("敌方")] Enemy,
        [LabelText("当前目标")] Target,
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

}