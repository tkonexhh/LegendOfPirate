using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_Critical : BuffModelHandler_Attribute
    {
        public BuffModelHandler_Critical(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            model.MoveSpeedAddRate.Value += m_Value;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.MoveSpeedAddRate.Value -= m_Value;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}