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

}