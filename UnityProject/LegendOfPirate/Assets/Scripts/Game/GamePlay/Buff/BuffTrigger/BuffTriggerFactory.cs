using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTriggerFactory
    {
        public static BuffTrigger CreateBuffTrigger(PassiveSkillTriggerType triggerType)
        {
            switch (triggerType)
            {
                case PassiveSkillTriggerType.Attack:
                    return new BuffTrigger_Attack();
                case PassiveSkillTriggerType.Hurt:
                    return new BuffTrigger_Hurt();
                case PassiveSkillTriggerType.Forver:
                    return new BuffTrigger_Forver();
                case PassiveSkillTriggerType.Time:
                    return new BuffTrigger_Time();
                case PassiveSkillTriggerType.Move:
                    return new BuffTrigger_Move();
            }

            return null;
        }
    }

}