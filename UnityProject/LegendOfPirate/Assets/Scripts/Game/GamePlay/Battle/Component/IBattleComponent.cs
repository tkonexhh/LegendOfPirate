using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBattleComponent
    {
        BattleMgr BattleMgr { get; set; }

        void Init();
        void OnBattleInit(BattleFieldConfigSO enemyConfigSO);
        void OnBattleStart();
        void OnBattleUpdate();
        void OnBattleEnd(bool isSuccess);
        void OnBattleClean();
    }

}