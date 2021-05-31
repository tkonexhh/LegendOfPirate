using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffAppendHandler_Reset : BuffAppendHandler
    {
        public override void HandleApped(Buff oldBuff, Buff newBuff)
        {
            oldBuff.time = newBuff.time;
        }
    }

}