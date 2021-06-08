using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class SkillFactory
    {
        public static Skill CreateSkill(SkillConfigSO configSO)
        {
            Skill skill = null;
            switch (configSO.SkillType)
            {
                case SkillType.Initiative:
                    skill = ObjectPool<InitiativeSkill>.S.Allocate();
                    skill = DealWithInitativeSkill(configSO, skill as InitiativeSkill);
                    break;
                case SkillType.Passive:
                    skill = ObjectPool<PassiveSkill>.S.Allocate();
                    skill = DealWithPassiveSkill(configSO, skill as PassiveSkill);
                    break;
            }

            skill.id = configSO.ID;
            skill.cd = configSO.CD;
            skill.name = configSO.name;
            skill.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);

            return skill;
        }

        private static InitiativeSkill DealWithInitativeSkill(SkillConfigSO configSO, InitiativeSkill skill)
        {
            skill.attacker = BattleAttackerFactory.CreateBattleAttacker(configSO.Attack.AttackType);
            skill.damageRange = DamageRangeFactory.CreateDamageRange(configSO.Attack.DamageRangeType, skill, configSO.Attack.RangeArgs);
            if (configSO.Attack.AttackType == AttackType.Shoot)//远程
            {
                BattleAttackerFactory.SetBullet((skill.attacker as BattleAttacker_Shoot), configSO.Attack.Bullet, configSO.Attack.BulletNum);
            }
            return skill;
        }


        private static PassiveSkill DealWithPassiveSkill(SkillConfigSO configSO, PassiveSkill skill)
        {
            skill.skillTrigger = SkillTriggerFactory.CreateSkillTrigger(configSO.PassiveSkillTriggerType);
            return skill;
        }
    }

}