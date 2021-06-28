using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_EvasionRate : BuffModelHandler_Attribute
    {

        public BuffModelHandler_EvasionRate(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            model.EvasionRate += m_Value;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.EvasionRate -= m_Value;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}