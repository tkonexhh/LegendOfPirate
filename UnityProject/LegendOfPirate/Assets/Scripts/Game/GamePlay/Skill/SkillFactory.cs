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
            Skill skill = ObjectPool<Skill>.S.Allocate();
            DealWithSkillAction(configSO.SkillActionConfigs, skill);
            skill.skillTrigger = SkillTriggerFactory.CreateSkillTrigger(configSO.SkillTriggerType, skill);
            skill.id = configSO.ID;
            skill.CD = configSO.CD;
            skill.name = configSO.name;
            skill.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);

            return skill;
        }

        // private static InitiativeSkill DealWithInitativeSkill(SkillConfigSO configSO, InitiativeSkill skill)
        // {
        //     skill.attacker = BattleAttackerFactory.CreateBattleAttacker(configSO.Attack.AttackType);
        //     skill.damageRange = DamageRangeFactory.CreateDamageRange(configSO.Attack.DamageRangeType, skill, configSO.Attack.RangeArgs);
        //     if (configSO.Attack.AttackType == AttackType.Shoot)//远程
        //     {
        //         BattleAttackerFactory.SetBullet((skill.attacker as BattleAttacker_Shoot), configSO.Attack.Bullet, configSO.Attack.BulletNum);
        //     }
        //     return skill;
        // }

        private static void DealWithSkillAction(List<SkillActionConfig> configs, Skill skill)
        {
            if (configs == null) return;
            skill.SkillActions = new List<SkillAction>();

            for (int i = 0; i < configs.Count; i++)
            {
                if (configs[i] is SkillActionConfig_AddBuff addBuffConfig) { skill.SkillActions.Add(CreateSkillAction_AddBuff(skill, addBuffConfig)); }
                if (configs[i] is SkillActionConfig_Damage damageConfig) { skill.SkillActions.Add(CreateSkillAction_Damage(skill, damageConfig)); }
            }
        }

        private static SkillAction CreateSkillAction_AddBuff(Skill skill, SkillActionConfig_AddBuff actionConfig)
        {
            return new SkillAction_AddBuff(actionConfig.buffConfigSO, SkillTargetFactory.CreateSkillTarget(actionConfig.target, skill));
        }

        private static SkillAction CreateSkillAction_Damage(Skill skill, SkillActionConfig_Damage actionConfig)
        {
            return new SkillAction_Damage(actionConfig.Damage, actionConfig.DamageType, SkillTargetFactory.CreateSkillTarget(actionConfig.target, skill));
        }

    }

}