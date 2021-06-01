using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffModelHandler_MoveSpeed : BuffModelHandler_Attribute
    {
        public BuffModelHandler_MoveSpeed(ModifyType modifyType, int value) : base(modifyType, value)
        {
        }

        public override void OnAddBuff(BattleRoleModel model)
        {
            if (m_ModifyType == ModifyType.Add)
            {
                model.MoveSpeed += m_Value;
            }
            else
            {
                model.MoveSpeed *= m_Value;
            }
        }
        public override void OnRemoveBuff(BattleRoleModel model)
        {
            if (m_ModifyType == ModifyType.Add)
            {
                model.MoveSpeed -= m_Value;
            }
            else
            {
                model.MoveSpeed /= m_Value;
            }
        }

        public override void OnAppendBuff(int appendNum, BattleRoleModel model)
        {

        }
    }

}