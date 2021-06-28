using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_ExtraHp : BuffModelHandler_Attribute
    {
        public BuffModelHandler_ExtraHp(int value) : base(value)
        {
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            float m_DeltaValue = m_Value * 0.01f;
            int addExtraHp = (int)(m_DeltaValue * model.BasicMaxHp);
            model.ExtraHP += addExtraHp;
        }
        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.ExtraHP = 0;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model)
        {

        }
    }

}