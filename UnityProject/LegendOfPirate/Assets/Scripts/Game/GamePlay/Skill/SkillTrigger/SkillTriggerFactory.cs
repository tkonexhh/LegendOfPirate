using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTriggerFactory
    {

        public static SkillTrigger CreateSkillTrigger(SkillTriggerType triggerType, Skill skill)
        {
            switch (triggerType)
            {
                case SkillTriggerType.OnCreate:
                    skill.CD = SkillDefine.INFINITETIME;//默认触发的技能CD一定为-1
                    return new SkillTrigger_Create();
                case SkillTriggerType.OnSpellStart:
                    return new SkillTrigger_Cast();
                case SkillTriggerType.OnAttack:
                    skill.CD = SkillDefine.INFINITETIME;//默认触发的技能CD一定为-1
                    return new SkillTrigger_Attack();
            }
            return null;
        }
    }

}