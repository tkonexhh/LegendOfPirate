using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BulletDefine
    {

    }

    [LabelText("子弹飞行类型")]
    public enum BulletMoveType
    {
        [LabelText("直线")] Linear,
        [LabelText("抛物线")] Parabolic,
    }

    [LabelText("子弹目标类型")]
    public enum BulletTargetType
    {
        [LabelText("单体目标")] Target,
        [LabelText("位置")] Position,
    }

}