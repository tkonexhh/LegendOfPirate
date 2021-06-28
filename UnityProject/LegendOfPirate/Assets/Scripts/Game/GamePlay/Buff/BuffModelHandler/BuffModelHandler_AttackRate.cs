using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_AttackRate : BuffModelHandler_Attribute
    {
        float m_DeltaValue;
        public BuffModelHandler_AttackRate(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            model.bounsATKSpeedRate += m_DeltaValue;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.bounsATKSpeedRate -= m_DeltaValue;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}