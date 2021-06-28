using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    public class BattleRoleModel : Model
    {
        public int BasicMaxHp;//基础最大生命值

        public float BasicMoveSpeed = 1;//移动速度
        public int BasicATK;//攻击力
        public float BasicATKTime = 2.0f;//基础攻击间隔


        public int CriticalRate = 0;//暴击率
        public float Amor = 0;//护甲
        public int EvasionRate = 0;//闪避率
        public float SkillATKRate = 0;//技能伤害
        public float SkillAmorRate = 0;//技能保护
        public float AtkHeal = 0;//吸血率
        public float ATKReflectRate = 0;//反伤率
        public float HPRecoverRate = 0;//生命回复率

    }

    public class BattleRoleRuntimeModel : BattleRoleModel
    {
        private int m_HP;//当前生命值
        public int Hp
        {
            get => m_HP;
            set
            {
                m_HP = value;
                m_HP = Mathf.Clamp(m_HP, 0, MaxHp);
                if (m_HP <= 0)
                {
                    IsDead.Value = true;
                }
            }
        }
        public int MaxHp;
        public float MaxHpAddRate = 0;
        public float ATKAddRate = 0;//攻击加成比例
        public float bounsATKSpeedRate = 0;//攻击加速率

        public float AtkTime => BasicATKTime / (1.0f + bounsATKSpeedRate);//最终攻击间隔

        public FloatReactiveProperty MoveSpeedAddRate = new FloatReactiveProperty(0);
        public int ExtraHP = 0;//额外生命值 护盾
        public StatusMask StatusMask = new StatusMask();//状态标识位


        public float MoveSpeed => BasicMoveSpeed * (1.0f + MoveSpeedAddRate.Value);
        public int ATK => (int)(BasicATK * (1.0f + ATKAddRate));
        public BoolReactiveProperty IsDead = new BoolReactiveProperty(false);

    }


    public class StatusMask
    {
        public StatusControlType mask = StatusControlType.None;

        public void AddStatus(StatusControlType statusType)
        {
            // Int32 add = 1 << (Int32)(statusType);
            mask = (mask) | statusType;
            // Debug.LogError(mask.ToString());
            // Debug.LogError("AddStatus Temp" + Convert.ToString(add, 2));
        }

        public void RemoveStatus(StatusControlType statusType)
        {
            // Int32 remove = ~(1 << (Int32)(statusType));
            // Debug.LogError("Remove Temp" + Convert.ToString(remove, 2));
            // mask = mask & remove;
            mask = mask ^ statusType;
            // Debug.LogError(mask.ToString());
        }

        public bool HasStatus(StatusControlType statusType)
        {
            return (mask & statusType) != 0; //((mask >> (Int32)statusType) & 1) == 1;
        }

        public override string ToString()
        {
            return mask.ToString();
        }
    }



}