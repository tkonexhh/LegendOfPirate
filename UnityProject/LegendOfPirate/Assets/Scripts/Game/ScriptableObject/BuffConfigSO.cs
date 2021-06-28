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
        [LabelText("持续时间")]
        [PropertyTooltip("-1 是永久持续"), Range(-1, 200)]
        public float Time;

        [LabelText("持续时间成长值")]
        public float TimeDelta;

        public BuffTriggerType BuffTriggerType;

        [ShowIf("BuffTriggerType", BuffTriggerType.Interval), LabelText("间隔时间")]
        public float Interval = 1;


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
        [LabelText("属性修饰列表")]
        [HideReferenceObjectPicker]
        public List<ModifierAttribute> ModifierAttributeLst = new List<ModifierAttribute>();
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
        [TextArea, LabelText("状态描述")]
        public string StatusDescription;

    }

    [LabelText("属性修饰子项")]
    public class ModifierAttribute
    {
        public AttributeType AttributeType;
        [LabelText("数值参数")]
        public int NumericValue;
        [LabelText("成长参数")]
        public int NumericValueDelta;
        // public BuffTriggerType triggerType;
    }

}