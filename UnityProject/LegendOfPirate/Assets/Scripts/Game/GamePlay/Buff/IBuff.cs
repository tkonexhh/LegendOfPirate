using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBuff
    {
        void OnAddBuff(BattleRoleModel model);
        void OnRemoveBuff(BattleRoleModel model);
    }

}