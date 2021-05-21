using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围
    /// </summary>
    public abstract class DamageRange
    {
        public abstract List<EntityBase> PickTargets();
    }

}