using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBuff
    {
        void OnAddBuff(BattleRoleRuntimeModel model);
        void OnRemoveBuff(BattleRoleRuntimeModel model);
    }

}