using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_Amor : BuffModelHandler_Attribute
    {
        private float m_DeltaValue = 0;
        public BuffModelHandler_Amor(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            Debug.LogError("BuffModelHandler_Amor +:" + m_DeltaValue);
            model.Amor += m_Value;
        }

        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.Amor -= m_Value;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}