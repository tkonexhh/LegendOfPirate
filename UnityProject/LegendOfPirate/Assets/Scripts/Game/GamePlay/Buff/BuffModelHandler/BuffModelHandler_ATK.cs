using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_ATK : BuffModelHandler_Attribute
    {
        public BuffModelHandler_ATK(ModifyType modifyType, float value) : base(modifyType, value)
        {
        }

        public override void OnAddBuff(BattleRoleModel model)
        {

        }
        public override void OnRemoveBuff(BattleRoleModel model)
        {

        }

        public override void OnAppendBuff(int appendNum, BattleRoleModel model)
        {
            //
        }
    }

}