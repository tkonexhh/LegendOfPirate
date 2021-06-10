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
        public DamageRange DamageRange { get; set; }//伤害范围

        public BattleRoleModel originData;
        public BattleRoleRuntimeModel buffedData;
        public float AtkRange { get; set; }//攻击距离

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

            controller.MonoReference.AstarAI.maxSpeed = buffedData.MoveSpeed;
            controller.MonoReference.AstarAI.endReachedDistance = AtkRange;
        }


        public void GetDamage(RoleDamagePackage damagePackage)
        {
            int damage = 0;
            switch (damagePackage.damageType)
            {
                case BattleDamageType.Normal:
                    //先计算闪避
                    if (RandomHelper.Range(0, 100) < buffedData.EvasionRate)
                    {
                        //闪避了
                        break;
                    }
                    damage = BattleHelper.CalcATKHurt(damagePackage.damage, buffedData);
                    break;
                case BattleDamageType.Skill:
                    damage = BattleHelper.CalcSkillHurt(damagePackage.damage, buffedData);
                    break;
            }
            buffedData.Hp -= damage;
            WorldUIPanel.S.ShowAbnormalInjuryText(controller.transform, damage);
            // Debug.LogError(buffedData.Hp + ":" + damage);
        }

    }

    /// <summary>
    /// 攻击包
    /// </summary>
    public class RoleDamagePackage
    {
        public BattleDamageType damageType;
        public int damage;
    }

    /// <summary>
    /// 攻击类型
    /// </summary>
    public enum BattleDamageType
    {
        Normal,//普通攻击
        Skill,//技能攻击
    }

}