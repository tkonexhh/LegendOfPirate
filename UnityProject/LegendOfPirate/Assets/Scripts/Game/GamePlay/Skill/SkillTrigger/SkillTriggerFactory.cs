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
                case PassiveSkillTriggerType.Forver:
                    return new SkillTrigger_Forver();
                case PassiveSkillTriggerType.Time:
                    return new SkillTrigger_Time();
                case PassiveSkillTriggerType.Move:
                    return new SkillTrigger_Move();
            }

            return null;
        }
    }

}