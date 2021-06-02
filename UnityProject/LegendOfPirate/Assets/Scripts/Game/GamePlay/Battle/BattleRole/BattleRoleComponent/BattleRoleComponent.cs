using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleRoleComponent
    {
        public virtual void OnBattleStart() { }
        public virtual void OnUpdate() { }

    }

}