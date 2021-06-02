using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillFactory
    {
        public static Skill CreateSkill(SkillConfigSO configSO)
        {
            Skill skill;
            switch (configSO.SkillType)
            {
                case SkillType.Initiative:
                    skill = new InitiativeSkill();
                    break;
                case SkillType.Passive:
                    skill = new PassiveSkill();
                    DealWithPassiveSkill(configSO, skill as PassiveSkill);
                    break;
                default:
                    skill = new Skill();
                    break;
            }

            skill.id = configSO.ID;
            skill.cd = configSO.CD;
            skill.name = configSO.name;
            skill.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);

            return skill;
        }

        private static PassiveSkill DealWithPassiveSkill(SkillConfigSO configSO, PassiveSkill skill)
        {
            skill.skillTrigger = SkillTriggerFactory.CreateSkillTrigger(configSO.PassiveSkillTriggerType);
            return skill;
        }
    }

}