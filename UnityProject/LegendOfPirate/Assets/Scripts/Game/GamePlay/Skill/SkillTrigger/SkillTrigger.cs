using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 被动技能触发
    /// </summary>
    public abstract class SkillTrigger
    {
        public delegate void OnSkillTrigger();
        public OnSkillTrigger onSkillTrigger;


        public abstract void Start(BattleRoleController controller);
        public abstract void Stop(BattleRoleController controller);
        protected void OnTrigger()
        {
            if (onSkillTrigger != null)
            {
                onSkillTrigger();
            }
        }

    }

}