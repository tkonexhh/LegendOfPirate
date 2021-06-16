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

    [LabelText("播放特效")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_PlayEffect : SkillActionConfig
    {
        [LabelText("特效资源")]
        public GameObject effect;
        public SkillTargetType targetType;
    }

    [LabelText("冲刺")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Sprint : SkillActionConfig
    {
        [LabelText("距离")] public float range = 10;
        [LabelText("速度")] public float speed = 20;
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

    #region  范围伤害
    [LabelText("范围伤害")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_RangeDamage : SkillActionConfig
    {
        public int Damage = 10;
        public SkillTargetType targetType;
        public RangeDamageConfig RangeDamage;
    }

    public class RangeDamageConfig
    {

    }

    [LabelText("圆形伤害")]
    public class RangeDamageConfig_Circle : RangeDamageConfig
    {
        [LabelText("半径")] public float Radius;
    }

    [LabelText("矩形伤害")]
    public class RangeDamageConfig_Rect : RangeDamageConfig
    {
        [LabelText("宽度")] public float Width;
        [LabelText("高度")] public float Height;
    }

    [LabelText("扇形伤害")]
    public class RangeDamageConfig_Sector : RangeDamageConfig
    {
        [LabelText("半径")] public float Radius;
        [LabelText("角度")] public float Dregee;
    }

    #endregion

    [LabelText("击退")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_HitBack : SkillActionConfig
    {
        [LabelText("距离")] public float Distance;
    }

    [LabelText("拉近")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Pull : SkillActionConfig
    {
        [LabelText("距离")] public float range = 10;
        [LabelText("速度")] public float speed = 20;
    }


    [LabelText("召唤")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Summon : SkillActionConfig
    {
        [LabelText("召唤角色")] public RoleConfigSO RoleConfigSO;
        [LabelText("持续时间")] public float lifeTime = 30;
        [LabelText("攻击比例")] public float ATKRate = 1;
        [LabelText("生命比例")] public float HPRate = 1;
    }

    [LabelText("发射子弹")]
    [HideReferenceObjectPicker]
    public class SkillActionConfig_Bullet : SkillActionConfig
    {
        [LabelText("伤害")] public int Damage;
        [LabelText("子弹配置")] public BulletConfigSO BulletConfigSO;

        public DamageRangeType DamageRangeType;
        [HideIf("DamageRangeType", DamageRangeType.Single), LabelText("伤害范围参数")]
        public string RangeArgs;
    }
}