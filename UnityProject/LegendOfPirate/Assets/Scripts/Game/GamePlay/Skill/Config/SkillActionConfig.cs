using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [HideReferenceObjectPicker]
    public class SkillActionConfig
    {

    }

    [LabelText("直接造成伤害")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Damage : SkillActionConfig
    {
        [LabelText("伤害量")]
        public int Damage;
        public BattleDamageType DamageType;
        [LabelText("目标")]
        public SkillTargetConfig target;
    }

    [LabelText("治愈")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Heal : SkillActionConfig
    {
        [LabelText("治愈量")]
        public int HealAmount;
        [LabelText("目标")]
        public SkillTargetConfig target;
    }

    [LabelText("添加Buff")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_AddBuff : SkillActionConfig
    {
        [LabelText("Buff配置")]
        public BuffConfigSO buffConfigSO;
        [LabelText("目标")]
        public SkillTargetConfig target;
    }

    [LabelText("播放声音")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_PlaySound : SkillActionConfig
    {
        [LabelText("音效资源")]
        public AudioClip audio;
        [LabelText("目标")]
        public SkillTargetConfig target;
    }


}