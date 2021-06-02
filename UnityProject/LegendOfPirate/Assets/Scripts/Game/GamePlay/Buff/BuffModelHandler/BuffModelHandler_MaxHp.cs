using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_MaxHp : BuffModelHandler_Attribute
    {
        private float m_DeltaValue;
        public BuffModelHandler_MaxHp(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            model.MaxHpAddRate += m_DeltaValue;
            model.MaxHp = (int)(model.BasicMaxHp * (1.0f + model.MaxHpAddRate));
        }

        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.MaxHpAddRate -= m_DeltaValue;
            model.MaxHp = (int)(model.BasicMaxHp / (1.0f + model.MaxHpAddRate));
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}