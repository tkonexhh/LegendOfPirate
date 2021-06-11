using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class SkillTargetConfig
    {
    }

    [LabelText("施法者")]
    public class SkillTargetConfig_Caster : SkillTargetConfig
    {

    }

    [LabelText("某个位置")]
    public class SkillTargetConfig_Postion : SkillTargetConfig
    {

    }
}