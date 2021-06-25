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
            skill.range = configSO.Range;
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
                if (configs[i] is SkillActionConfig_Delay delayConfig) { skill.SkillActions.Add(CreateSkillAction_Delay(delayConfig)); }
                if (configs[i] is SkillActionConfig_AddBuff addBuffConfig) { skill.SkillActions.Add(CreateSkillAction_AddBuff(addBuffConfig)); }
                if (configs[i] is SkillActionConfig_Damage damageConfig) { skill.SkillActions.Add(CreateSkillAction_Damage(skill, damageConfig)); }
                if (configs[i] is SkillActionConfig_Heal healConfig) { skill.SkillActions.Add(CreateSkillAction_Heal(skill, healConfig)); }
                if (configs[i] is SkillActionConfig_RangeHeal rangeHealConfig) { skill.SkillActions.Add(CreateSkillAction_RangeHeal(skill, rangeHealConfig)); }
                if (configs[i] is SkillActionConfig_PlaySound soundConfig) { skill.SkillActions.Add(CreateSkillAction_PlaySound(skill, soundConfig)); }//TODO
                if (configs[i] is SkillActionConfig_PlayEffect effectConfig) { skill.SkillActions.Add(CreateSkillAction_PlayEffect(skill, effectConfig)); }//TODO
                if (configs[i] is SkillActionConfig_Sprint sprintConfig) { skill.SkillActions.Add(CreateSkillAction_Sprint(skill, sprintConfig)); }
                if (configs[i] is SkillActionConfig_FlashForward flashForwardConfig) { skill.SkillActions.Add(CreateSkillAction_FlashForward(flashForwardConfig)); }
                if (configs[i] is SkillActionConfig_FlashBackward flashBackwardConfig) { skill.SkillActions.Add(CreateSkillAction_FlashBackward(flashBackwardConfig)); }
                if (configs[i] is SkillActionConfig_RangeDamage rangeDamageConfig) { skill.SkillActions.Add(CreateSkillAction_RangeDamage(rangeDamageConfig)); }
                if (configs[i] is SkillActionConfig_HitBack hitbackConfig) { skill.SkillActions.Add(CreateSkillAction_Hitback(hitbackConfig)); }
                if (configs[i] is SkillActionConfig_Pull pullConfig) { skill.SkillActions.Add(CreateSkillAction_Pull(pullConfig)); }
                if (configs[i] is SkillActionConfig_Summon summonConfig) { skill.SkillActions.Add(CreateSkillAction_Summon(summonConfig)); }
                if (configs[i] is SkillActionConfig_Bullet bulletConfig) { skill.SkillActions.Add(CreateSkillAction_Bullet(bulletConfig)); }
            }
        }

        private static SkillAction CreateSkillAction_Delay(SkillActionConfig_Delay config)
        {
            return new SkillAction_Delay(config.Delay);
        }

        private static SkillAction CreateSkillAction_AddBuff(SkillActionConfig_AddBuff actionConfig)
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

        private static SkillAction CreateSkillAction_RangeHeal(Skill skill, SkillActionConfig_RangeHeal config)
        {
            RangeDamage rangeDamage = RangeDamageConfig.CreateRangeDamage(config.RangeDamage);
            return new SkillAction_RangeHeal(rangeDamage, config.HealAmount);
        }

        private static SkillAction CreateSkillAction_PlaySound(Skill skill, SkillActionConfig_PlaySound config)
        {
            return new SkillAction_PlaySound(config.audio, config.targetType);
        }

        private static SkillAction CreateSkillAction_PlayEffect(Skill skill, SkillActionConfig_PlayEffect config)
        {
            return new SkillAction_PlayEffect(config.effect, config.targetType);
        }

        private static SkillAction CreateSkillAction_Sprint(Skill skill, SkillActionConfig_Sprint config)
        {
            return new SkillAction_Sprint(config.range, config.speed, config.targetType);
        }

        private static SkillAction CreateSkillAction_FlashForward(SkillActionConfig_FlashForward config)
        {
            return new SkillAction_FlashForward(config.fashForwardType);
        }

        private static SkillAction CreateSkillAction_FlashBackward(SkillActionConfig_FlashBackward config)
        {
            return new SkillAction_FlashBackward(config.distance);
        }

        private static SkillAction CreateSkillAction_RangeDamage(SkillActionConfig_RangeDamage config)
        {
            RangeDamage rangeDamage = RangeDamageConfig.CreateRangeDamage(config.RangeDamage);
            return new SkillAction_RangeDamage(rangeDamage, config.targetType, config.Damage);
        }

        private static SkillAction CreateSkillAction_Hitback(SkillActionConfig_HitBack config)
        {
            return new SkillAction_HitBack(config.Distance);
        }

        private static SkillAction CreateSkillAction_Pull(SkillActionConfig_Pull config)
        {
            return new SkillAction_Pull(config.range, config.speed);
        }

        private static SkillAction CreateSkillAction_Summon(SkillActionConfig_Summon config)
        {
            return new SkillAction_Summon(config.RoleConfigSO, config.ATKRate, config.HPRate, config.lifeTime);
        }

        private static SkillAction CreateSkillAction_Bullet(SkillActionConfig_Bullet config)
        {
            BattleMgr.S.Pool.AddGameObjectToPool(config.BulletConfigSO.Prefab);//将技能子弹添加到pool
            return new SkillAction_Bullet(config.Damage, config.BulletConfigSO, config.DamageRangeType, config.RangeDamage);
        }



    }

}