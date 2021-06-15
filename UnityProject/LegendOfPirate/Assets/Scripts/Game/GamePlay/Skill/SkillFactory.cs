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


        private static void DealWithSkillAction(List<SkillActionConfig> configs, Skill skill)
        {
            if (configs == null) return;
            skill.SkillActions = new List<SkillAction>();

            for (int i = 0; i < configs.Count; i++)
            {
                if (configs[i] is SkillActionConfig_AddBuff addBuffConfig) { skill.SkillActions.Add(CreateSkillAction_AddBuff(skill, addBuffConfig)); }
                if (configs[i] is SkillActionConfig_Damage damageConfig) { skill.SkillActions.Add(CreateSkillAction_Damage(skill, damageConfig)); }
                if (configs[i] is SkillActionConfig_Heal healConfig) { skill.SkillActions.Add(CreateSkillAction_Heal(skill, healConfig)); }
                if (configs[i] is SkillActionConfig_PlaySound soundConfig) { skill.SkillActions.Add(CreateSkillAction_PlaySound(skill, soundConfig)); }//TODO
                if (configs[i] is SkillActionConfig_Sprint sprintConfig) { skill.SkillActions.Add(CreateSkillAction_Sprint(skill, sprintConfig)); }
            }
        }

        private static SkillAction CreateSkillAction_AddBuff(Skill skill, SkillActionConfig_AddBuff actionConfig)
        {
            return new SkillAction_AddBuff(actionConfig.buffConfigSO, actionConfig.targetType);
        }

        private static SkillAction CreateSkillAction_Damage(Skill skill, SkillActionConfig_Damage actionConfig)
        {
            return new SkillAction_Damage(actionConfig.Damage, actionConfig.DamageType, actionConfig.targetType);
        }

        private static SkillAction CreateSkillAction_Heal(Skill skill, SkillActionConfig_Heal healConfig)
        {
            return new SkillAction_Heal(healConfig.HealAmount, healConfig.targetType);
        }

        private static SkillAction CreateSkillAction_PlaySound(Skill skill, SkillActionConfig_PlaySound config)
        {
            return new SkillAction_PlaySound(config.audio, config.targetType);
        }

        private static SkillAction CreateSkillAction_Sprint(Skill skill, SkillActionConfig_Sprint config)
        {
            return new SkillAction_Sprint(config.range, config.speed, config.targetType);
        }
    }

}