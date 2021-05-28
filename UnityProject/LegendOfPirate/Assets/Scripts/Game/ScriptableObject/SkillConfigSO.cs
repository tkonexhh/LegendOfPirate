using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/SkillConfigSO", fileName = "new_SkillConfigSO")]
    public class SkillConfigSO : SerializedScriptableObject
    {
        [LabelText("技能ID")]
        public int ID = 0;

        [LabelText("技能名称")]
        public string SkillName = "SkillName";

        [LabelText("技能范围")]
        public float Range = 1;

        public SkillType SkillType;

        [LabelText("附加Buff列表")]
        [PropertyTooltip("主动技能受击附属Buff以及被动技能触发Buff")]
        public List<BuffConfigSO> ChildBuffs = new List<BuffConfigSO>();

        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();

        [LabelText("技能CD")]
        [PropertyTooltip("CD:主动技能CD 被动技能间隔CD")]
        public float CD;

        [Space(40)]
        [LabelText("技能执行")]
        public GameObject SkillEffect;
        [LabelText("技能音效")]
        public AudioClip SkillAudio;

        //===被动技能相关
        [ShowIf("SkillType", SkillType.Passive), Space(40)]
        public PassiveSkillTriggerType PassiveSkillTriggerType;
        //===

        //===主动技能相关
        [HideReferenceObjectPicker]
        [ShowIf("SkillType", SkillType.Initiative), Space(40)]
        public Attack Attack;//=new Attack();
        //===
    }


    [LabelText("选择器")]
    public class PickTarget
    {
        public PickTargetType PickTargetType;
        public SensorTypeEnum SensorTypeEnum;
        [PropertyRange(1, 30), LabelText("数量")]
        public int PickNum = 1;
    }

    [LabelText("攻击方式")]
    public class Attack
    {
        public AttackType AttackType;
        public DamageType DamageType;


        //===范围攻击
        [ShowIf("DamageType", DamageType.Range)]
        public DamageRangeType DamageRangeType;
        [ShowIf("DamageType", DamageType.Range), LabelText("伤害范围参数")]
        public string RangeArgs;
        //===

        //===远程攻击
        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹对象")]
        public GameObject Bullet;

        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹数量")]
        public int BulletNum = 1;
        //===
    }



}