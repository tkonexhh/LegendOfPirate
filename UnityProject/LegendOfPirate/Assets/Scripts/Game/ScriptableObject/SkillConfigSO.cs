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
        [OnValueChanged("OnSkillTriggerTypeChange")]
        public SkillTriggerType SkillTriggerType;
        [LabelText("触发CD")]
        [HideIf("SkillTriggerType", SkillTriggerType.OnSpellStart)]
        public float TriggerCD;


        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();


        [BoxGroup("技能执行操作")]
        [LabelText("技能操作")]
        public List<SkillActionConfig> SkillActionConfigs = new List<SkillActionConfig>();


        [Space(50)]
        [TextArea, LabelText("技能描述")]
        public string StatusDescription;


        private void OnSkillTriggerTypeChange()
        {
            if (SkillTriggerType == SkillTriggerType.OnAttack || SkillTriggerType == SkillTriggerType.OnCreate)
            {
                CD = -1;
            }
            else
            {
                CD = 1;
            }
        }

    }


    [LabelText("选择器")]
    public class PickTarget
    {
        public PickTargetType PickTargetType;
        public SensorTypeEnum SensorTypeEnum;
    }





}