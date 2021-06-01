using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleModel : Model
    {
        public int Hp;//生命值
        public int MaxHp;//最大生命值
        public int ATK;//攻击力
        public float MoveSpeed = 1;//移动速度
        public float baseATKRate = 1.0f;//基础攻击速率
        public float bounsATKRate = 0;//攻击加速

        public int CriticalRate = 0;//暴击率
        public float Amor = 0;//护甲
        public float EvasionRate = 0;//闪避率
        public float SkillATKRate = 0;//技能伤害
        public float SkillAmorRate = 0;//技能保护
        public float ATKResponseRate = 0;//吸血率
        public float ATKReflectRate = 0;//反伤率
        public float HPRecoverRate = 0;//生命回复率
        public float ExtraHP = 0;//额外生命值 护盾

        public StatusMask StatusMask;//状态标识位
    }

    public class StatusMask
    {
        public Int32 mask = 0;

        public void AddStatus(StatusControlType statusType)
        {
            Int32 add = 1 << (Int32)(statusType);
            mask = (mask) | add;
            // Debug.LogError("AddStatus Temp" + Convert.ToString(add, 2));
        }

        public void RemoveStatus(StatusControlType statusType)
        {
            Int32 remove = ~(1 << (Int32)(statusType));
            // Debug.LogError("Remove Temp" + Convert.ToString(remove, 2));
            mask = mask & remove;
        }

        public bool HasStatus(StatusControlType statusType)
        {
            return ((mask >> (Int32)statusType) & 1) == 1;
        }

        public override string ToString()
        {
            return Convert.ToString(mask, 2);
        }
    }



}