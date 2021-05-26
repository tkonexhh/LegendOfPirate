using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class AbstractBattleComponent : IBattleComponent
    {
        public virtual void OnBattleInit() { }
        public virtual void OnBattleStart() { }
        public virtual void OnBattleUpdate() { }
        public virtual void OnBattleClean() { }
    }

}