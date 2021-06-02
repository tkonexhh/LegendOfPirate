using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTriggerFactory
    {
        public static SkillTrigger CreateSkillTrigger(PassiveSkillTriggerType triggerType)
        {
            switch (triggerType)
            {
                case PassiveSkillTriggerType.Attack:
                    return new SkillTrigger_Attack();
                case PassiveSkillTriggerType.Hurt:
                    return new SkillTrigger_Hurt();
            }

            return null;
        }
    }

}