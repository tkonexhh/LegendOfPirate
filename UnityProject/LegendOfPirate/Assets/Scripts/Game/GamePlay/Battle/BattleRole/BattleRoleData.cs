using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleData : BattleRoleComponent
    {
        public IBattleSensor Sensor { get; set; }//索敌方式
        public BattleAttacker Attacker { get; set; }//攻击方式
        public RangeDamage RangeDamage { get; set; }//范围伤害处理

        public BattleRoleModel originData;
        public BattleRoleRuntimeModel buffedData;
        public float AtkRange { get; set; }//攻击距离
        public float ColliderRange { get; set; }//碰撞距离
        public bool IsDead => buffedData.Hp <= 0;

        public BattleRoleData(BattleRoleController controller) : base(controller)
        {
            // Sensor = BattleSensorFactory.CreateBattleSensor(PickTargetType.Enemy, SensorTypeEnum.Nearest);
            // Attacker = new BattleAttacker_Lock();
            // DamageRange = new DamageRange_Target(controller);
        }

        public override void OnBattleStart()
        {
            buffedData = new BattleRoleRuntimeModel();
            buffedData.BasicATK = 10;
            buffedData.CriticalRate = 10;
            buffedData.BasicMaxHp = 100;
            buffedData.MaxHp = 100;
            buffedData.Hp = 90;
            buffedData.BasicMoveSpeed = 4.0f;
            buffedData.ATKReflectRate = 0.1f;//伤害反伤

            controller.MonoReference.AstarAI.maxSpeed = buffedData.MoveSpeed;
            controller.MonoReference.AstarAI.endReachedDistance = AtkRange;

            controller.MonoReference.Collider.radius = ColliderRange;
        }

        public override void OnDestroy()
        {
        }

        public void GetDamage(RoleDamagePackage damagePackage)
        {
            if (buffedData.StatusMask.HasStatus(StatusControlType.HurtForbid))
            {
                //伤害无效状态
                return;
            }

            int damage = 0;
            switch (damagePackage.damageType)
            {
                case BattleDamageType.Normal://普通伤害
                    //先计算闪避
                    if (RandomHelper.Range(0, 100) < buffedData.EvasionRate)
                    {
                        //闪避了
                        break;
                    }
                    damage = BattleHelper.CalcATKHurt(damagePackage.damage, buffedData);

                    //计算反伤
                    if (buffedData.ATKReflectRate > 0.0f)
                    {
                        int reflectDamage = (int)(damage * buffedData.ATKReflectRate);
                        RoleDamagePackage damagePackage_reflect = new RoleDamagePackage(controller);
                        damagePackage_reflect.damageType = BattleDamageType.Reflect;
                        damagePackage_reflect.damage = reflectDamage;
                        BattleMgr.S.SendDamage(damagePackage.owner, damagePackage_reflect);
                    }

                    if (damagePackage.owner.Data.buffedData.ATKResponseRate > 0.0f)//计算吸血
                    {
                        int addHp = (int)(damage * damagePackage.owner.Data.buffedData.ATKResponseRate);//吸血量
                        damagePackage.owner.Data.Heal(addHp);
                    }


                    break;
                case BattleDamageType.Skill://技能伤害
                    damage = BattleHelper.CalcSkillHurt(damagePackage.damage, buffedData);
                    break;

                case BattleDamageType.Reflect://反弹伤害
                    damage = damagePackage.damage;
                    break;
            }

            Hurt(damage);
            // WorldUIPanel.S.ShowAbnormalInjuryText(controller.transform, damage);
            // Debug.LogError(buffedData.Hp + ":" + damage);
        }


        public void Heal(int amount)
        {
            buffedData.Hp += amount;
        }

        public void Hurt(int damage)
        {
            if (buffedData.ExtraHP > 0)//有额外生命值
            {
                buffedData.ExtraHP -= damage;
                if (buffedData.ExtraHP < 0)
                {
                    damage = Mathf.Abs(buffedData.ExtraHP);//额外生命值不够的话，在扣血
                    buffedData.ExtraHP = 0;
                }
            }
            buffedData.Hp -= damage;
        }

    }

    /// <summary>
    /// 攻击包
    /// </summary>
    public class RoleDamagePackage
    {
        public BattleRoleController owner { get; private set; }
        public BattleDamageType damageType;
        public int damage;


        public RoleDamagePackage(BattleRoleController owner)
        {
            this.owner = owner;
        }
    }

    /// <summary>
    /// 攻击类型
    /// </summary>
    public enum BattleDamageType
    {
        Normal,//普通攻击
        Skill,//技能攻击
        Reflect,//反弹伤害
    }

}