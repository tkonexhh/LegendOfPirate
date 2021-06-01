using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_MoveSpeed : BuffModelHandler_Attribute
    {
        private float m_DeltaValue = 0;
        public BuffModelHandler_MoveSpeed(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            m_DeltaValue = m_Value * 0.01f;
            model.MoveSpeedAddRate.Value += m_DeltaValue;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.MoveSpeedAddRate.Value -= m_DeltaValue;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}