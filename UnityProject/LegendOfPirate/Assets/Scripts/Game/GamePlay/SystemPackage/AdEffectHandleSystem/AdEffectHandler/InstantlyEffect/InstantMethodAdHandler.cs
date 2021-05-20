using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class InstantMethodAdHandler : InstantlyAdEffectHandler
    {
        public InstantMethodAdHandler(AdType adType) : base(adType)
        {

        }

        public override void Handle(object[] args)
        {
            base.Handle(args);
            Action CallBackMethod = null;
            if (args.Length > 0)
            {
                CallBackMethod = (Action)args[0];
            }

            CallBackMethod?.Invoke();

        }
    }

}