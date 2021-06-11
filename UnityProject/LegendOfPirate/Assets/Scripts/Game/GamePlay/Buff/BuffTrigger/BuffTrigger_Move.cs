using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTrigger_Move : BuffTrigger
    {
        public override void Start(Buff buff)
        {
            base.Start(buff);
            buff.Owner.AI.onMove += OnTrigger;
        }

        public override void Stop(Buff buff)
        {
            base.Stop(buff);
            buff.Owner.AI.onMove -= OnTrigger;
        }
    }

}