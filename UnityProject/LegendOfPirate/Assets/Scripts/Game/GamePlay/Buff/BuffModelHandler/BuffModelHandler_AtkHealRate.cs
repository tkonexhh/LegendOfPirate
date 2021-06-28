using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_AtkHealRate : BuffModelHandler_Attribute
    {
        float m_DeltaValue;
        public BuffModelHandler_AtkHealRate(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            model.AtkHeal += m_DeltaValue;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.AtkHeal -= m_DeltaValue;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}