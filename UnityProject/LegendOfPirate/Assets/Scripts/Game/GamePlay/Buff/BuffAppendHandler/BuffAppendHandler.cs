using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BuffAppendHandler
    {
        public abstract void HandleApped(Buff oldBuff, Buff newBuff);
    }

}