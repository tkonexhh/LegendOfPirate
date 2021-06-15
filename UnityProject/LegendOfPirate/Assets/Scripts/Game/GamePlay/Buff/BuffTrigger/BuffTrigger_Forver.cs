using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 永久触发被动技能
    /// 所触发的buff时间应该为-1
    /// </summary>
    public class BuffTrigger_Forver : BuffTrigger
    {
        public override void Start(Buff buff)
        {
            base.Start(buff);
            OnTrigger();
        }

        public override void Stop(Buff buff)
        {
            base.Stop(buff);
        }
    }

}