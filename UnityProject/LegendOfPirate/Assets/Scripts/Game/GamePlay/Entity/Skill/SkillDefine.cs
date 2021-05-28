using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class SkillDefine
    {

    }

    [LabelText("属性类型")]
    public enum AttributeType
    {
        [LabelText("空")] None,
        [LabelText("生命值")] Hp,
        [LabelText("最大生命值")] MaxHp,
        [LabelText("攻击力")] AP,
        [LabelText("移动速度")] MoveSpeed,
        [LabelText("攻击间隔")] AttackRate,
        [LabelText("护甲")] Amor,
        [LabelText("暴击")] Critical,
    }

    [LabelText("修饰类型")]
    public enum ModifyType
    {
        [LabelText("数值")] Add,
        [LabelText("百分比")] PercentAdd,
    }

    [LabelText("行为禁制")]
    public enum ActionControlType
    {
        [LabelText("（空）")] None = 0,
        [LabelText("移动禁止")] MoveForbid = 1 << 1,
        [LabelText("施法禁止")] SkillForbid = 1 << 2,
        [LabelText("攻击禁止")] AttackForbid = 1 << 3,

    }

    [System.Serializable]
    public class ActionControl
    {
        public ActionControlType type;
        public bool isOn;
    }
}