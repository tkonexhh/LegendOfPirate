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
        [LabelText("BuffID")]
        public int ID = 0;
        [LabelText("Buff名称")]
        public string Name = "BuffName";

        //---
        [ToggleGroup("EnabledStateModify", "状态控制")]
        public bool EnabledStateModify;
        [ToggleGroup("EnabledStateModify")]
        public List<ActionControl> ActionControls = new List<ActionControl>();
        //===


        //---
        [ToggleGroup("EnabledAttributeModify", "属性修饰")]
        public bool EnabledAttributeModify;

        [ToggleGroup("EnabledAttributeModify"), LabelText("持续时间")]
        [PropertyTooltip("-1 是永久持续"), Range(-1, 99999)]
        public float Time;

        [ToggleGroup("EnabledAttributeModify")]
        public AttributeType AttributeType;
        [ToggleGroup("EnabledAttributeModify"), LabelText("数值参数")]
        public int NumericValue;
        [ToggleGroup("EnabledAttributeModify")]
        public ModifyType ModifyType;

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


        [Space(50)]
        [LabelText("状态特效")]
        public GameObject ParticleEffect;

        [LabelText("状态音效")]
        public AudioClip Audio;



        [Space(50)]
        [TextArea, LabelText("状态描述")]
        public string StatusDescription;

    }

}