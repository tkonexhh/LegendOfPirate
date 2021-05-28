using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BuffDefine
    {

    }


    [LabelText("Buff持续时间叠加类型")]
    public enum BuffAppendType
    {

        [LabelText("刷新")] Reset,
        [LabelText("累加")] Additive,
        [LabelText("继续")] Continue,
    }

    [LabelText("触发方式")]
    public enum BuffTriggerType
    {
        [LabelText("立即触发")] Instant,
        [LabelText("间隔触发")] Interval,
    }

    [LabelText("行为禁制类型")]
    public enum ActionControlType
    {
        [LabelText("（空）")] None = 0,
        [LabelText("移动禁止")] MoveForbid = 1 << 1,
        [LabelText("施法禁止")] SkillForbid = 1 << 2,
        [LabelText("攻击禁止")] AttackForbid = 1 << 3,

    }

    [System.Serializable, LabelText("行为禁制")]
    public class ActionControl
    {
        public ActionControlType type;
        public bool isOn;
    }

}