using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_ATK : BuffModelHandler_Attribute
    {
        private float m_DeltaValue;
        public BuffModelHandler_ATK(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            model.ATKAddRate += m_DeltaValue;
        }

        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.ATKAddRate -= m_DeltaValue;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {
            //
        }
    }

}