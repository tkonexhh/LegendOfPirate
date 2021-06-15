using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    /// <summary>
    /// 被动技能触发
    /// </summary>
    public abstract class BuffTrigger
    {

        public Run onBuffTrigger;

        public virtual void Start(Buff buff)
        {
            onBuffTrigger += buff.ExcuteBuff;
        }

        public virtual void Stop(Buff buff)
        {
            onBuffTrigger -= buff.ExcuteBuff;
        }

        protected void OnTrigger()
        {
            if (onBuffTrigger != null)
            {
                onBuffTrigger();
            }
        }

    }

}