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
                    break;
                default:
                    skill = new Skill();
                    break;
            }

            skill.id = configSO.ID;
            skill.name = configSO.name;
            skill.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.SensorTypeEnum);

            return null;
        }
    }

}