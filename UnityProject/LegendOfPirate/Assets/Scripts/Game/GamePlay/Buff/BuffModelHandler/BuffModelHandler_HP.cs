using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_HP : BuffModelHandler_Attribute
    {

        public BuffModelHandler_HP(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            Debug.LogError("BuffModelHandler_HP +:" + m_Value);
            model.Hp += m_Value;
        }

        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {

        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}