using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_MoveSpeed : BuffModelHandler_Attribute
    {
        private float m_DeltaValue = 0;
        public BuffModelHandler_MoveSpeed(ModifyType modifyType, float value) : base(modifyType, value)
        {
        }

        public override void OnAddBuff(BattleRoleModel model)
        {
            if (m_ModifyType == ModifyType.Add)
            {
                m_DeltaValue = m_Value;
            }
            else
            {
                m_DeltaValue = model.MoveSpeed * (m_Value * 0.01f);
            }

            model.MoveSpeed += m_DeltaValue;
        }
        public override void OnRemoveBuff(BattleRoleModel model)
        {
            model.MoveSpeed -= m_DeltaValue;
        }

        public override void OnAppendBuff(int appendNum, BattleRoleModel model)
        {

        }
    }

}