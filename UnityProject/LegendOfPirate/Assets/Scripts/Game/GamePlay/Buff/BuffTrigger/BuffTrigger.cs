using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 被动技能触发
    /// </summary>
    public abstract class BuffTrigger
    {
        public delegate void OnBuffTrigger();
        public OnBuffTrigger onBuffTrigger;


        public abstract void Start(BattleRoleController controller);
        public abstract void Stop(BattleRoleController controller);
        protected void OnTrigger()
        {
            if (onBuffTrigger != null)
            {
                onBuffTrigger();
            }
        }

    }

}