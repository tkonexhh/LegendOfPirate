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

    [LabelText("延时")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Delay : SkillActionConfig
    {
        [LabelText("延时时间")]
        public float Delay = 1;
    }

    [LabelText("直接造成伤害")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Damage : SkillActionConfig
    {
        [LabelText("伤害量")]
        public int Damage;
        public BattleDamageType DamageType;
        public SkillTargetType targetType;
    }

    [LabelText("治愈")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Heal : SkillActionConfig
    {
        [LabelText("治愈量")]
        public int HealAmount = 10;
        public SkillTargetType targetType;
    }

    [LabelText("直接添加Buff")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_AddBuff : SkillActionConfig
    {
        [LabelText("Buff配置")]
        public BuffConfigSO buffConfigSO;
        public SkillTargetType targetType;
    }

    [LabelText("播放声音")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_PlaySound : SkillActionConfig
    {
        [LabelText("音效资源")]
        public AudioClip audio;
        public SkillTargetType targetType;
    }

    [LabelText("冲刺")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Sprint : SkillActionConfig
    {
        public float range = 10;
        public float speed = 20;
        public SkillTargetType targetType;
    }

    [LabelText("闪现冲刺")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_FlashForward : SkillActionConfig
    {
        public SkillFashForwardType fashForwardType;
    }

    [LabelText("闪现冲刺类型")]
    public enum SkillFashForwardType
    {
        [LabelText("身前")] Front,
        [LabelText("身后")] Back,
    }

    [LabelText("闪现躲避")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_FlashBackward : SkillActionConfig
    {
        [LabelText("距离")]
        public float distance = 4;
    }
}