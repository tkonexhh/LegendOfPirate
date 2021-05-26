using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBattleComponent
    {
        void Init();
        void OnBattleInit();
        void OnBattleStart();
        void OnBattleUpdate();
        void OnBattleClean();
    }

}