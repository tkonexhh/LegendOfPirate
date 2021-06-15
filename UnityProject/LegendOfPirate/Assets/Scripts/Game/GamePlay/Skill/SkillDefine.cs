using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class SkillDefine
    {
        public const int INFINITETIME = -1;
    }

    [LabelText("释放对象(完成)")]
    public enum PickTargetType
    {
        [LabelText("敌方")] Enemy,
        [LabelText("己方")] Our,
        [LabelText("自身")] Self,
        [LabelText("当前目标")] Target,
    }


    [LabelText("技能触发类型")]
    public enum SkillTriggerType
    {
        [LabelText("技能施法开始")] OnSpellStart,
        [LabelText("默认添加")] OnCreate,
    }

    public enum SkillTargetType
    {
        [LabelText("施法者")] Caster,
        [LabelText("目标")] Target,
        // [LabelText("攻击者")] Attacker,
    }
}