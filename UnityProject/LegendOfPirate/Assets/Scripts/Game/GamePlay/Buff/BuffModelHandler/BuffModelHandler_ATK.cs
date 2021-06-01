using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_ATK : BuffModelHandler_Attribute
    {
        public BuffModelHandler_ATK(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {

        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {

        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {
            //
        }
    }

}