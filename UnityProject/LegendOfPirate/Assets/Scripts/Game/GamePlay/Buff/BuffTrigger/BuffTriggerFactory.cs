using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTriggerFactory
    {
        public static BuffTrigger CreateBuffTrigger(BuffTriggerType triggerType)
        {
            switch (triggerType)
            {
                case BuffTriggerType.Attack:
                    return new BuffTrigger_Attack();
                case BuffTriggerType.Hurt:
                    return new BuffTrigger_Hurt();
                case BuffTriggerType.Create:
                    return new BuffTrigger_Forver();
                case BuffTriggerType.Interval:
                    return new BuffTrigger_Time();
                case BuffTriggerType.Move:
                    return new BuffTrigger_Move();
            }

            return null;
        }
    }

}