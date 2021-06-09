using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/BuffConfigSO", fileName = "new_BuffConfigSO")]
    public class BuffConfigSO : SerializedScriptableObject
    {
        private const string Group_Name_Effect = "效果";

        [LabelText("BuffID")]
        public int ID = 0;
        [LabelText("Buff名称")]
        public string Name = "BuffName";
        [LabelText("持续时间")]
        [PropertyTooltip("-1 是永久持续"), Range(-1, 200)]
        public float Time;

        //---
        [ToggleGroup("EnabledStateModify", "状态控制")]
        public bool EnabledStateModify;
        [ToggleGroup("EnabledStateModify")]
        public StatusControlType StatusControls;
        //===


        //---
        [ToggleGroup("EnabledAttributeModify", "属性修饰")]
        public bool EnabledAttributeModify;


        [ToggleGroup("EnabledAttributeModify")]
        public AttributeType AttributeType;
        [ToggleGroup("EnabledAttributeModify"), LabelText("数值参数")]
        public int NumericValue;
        // [ToggleGroup("EnabledAttributeModify")]
        // public ModifyType ModifyType;

        [ToggleGroup("EnabledAttributeModify")]
        public BuffTriggerType triggerType;

        [ToggleGroup("EnabledAttributeModify"), ShowIf("triggerType", BuffTriggerType.Interval), LabelText("间隔时间")]
        public float Interval = 1;

        //===

        //---
        [ToggleGroup("EnabledAppend", "叠加处理")]
        public bool EnabledAppend;

        [ToggleGroup("EnabledAppend")]
        public BuffAppendType AppendType;

        [ToggleGroup("EnabledAppend"), LabelText("最大叠加层数"), Range(1, 100)]
        public int MaxAppendNum = 1;
        //===


        [BoxGroup(Group_Name_Effect)]
        [LabelText("状态特效")]
        public GameObject ParticleEffect;
        [BoxGroup(Group_Name_Effect)]
        [LabelText("状态音效")]
        public AudioClip Audio;



        [Space(50)]
        [TextArea, LabelText("状态描述")]
        public string StatusDescription;

    }

}