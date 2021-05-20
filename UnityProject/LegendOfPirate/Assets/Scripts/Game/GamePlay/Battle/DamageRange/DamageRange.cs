using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class DamageRange
    {
        public abstract List<EntityBase> PickTargets();
    }

}