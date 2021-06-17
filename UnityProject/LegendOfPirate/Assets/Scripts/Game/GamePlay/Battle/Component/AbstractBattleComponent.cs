using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class AbstractBattleComponent : IBattleComponent
    {
        public BattleMgr BattleMgr { get; set; }
        public virtual void Init() { }
        public virtual void OnBattleInit(BattleFieldConfigSO enemyConfigSO) { }
        public virtual void OnBattleStart() { }
        public virtual void OnBattleUpdate() { }
        public virtual void OnBattleEnd(bool isSuccess) { }
        public virtual void OnBattleClean() { }
    }

}