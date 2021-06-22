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

        [LabelText("技能CD")]
        [PropertyTooltip("CD:主动技能CD 被动技能间隔CD")]
        public float CD;


        public SkillTriggerType SkillTriggerType;
        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();


        [BoxGroup("技能执行操作")]
        [LabelText("技能操作")]
        public List<SkillActionConfig> SkillActionConfigs = new List<SkillActionConfig>();


    }


    [LabelText("选择器")]
    public class PickTarget
    {
        public PickTargetType PickTargetType;
        public SensorTypeEnum SensorTypeEnum;

        public RangeDamageConfig RangeDamage;
    }





}