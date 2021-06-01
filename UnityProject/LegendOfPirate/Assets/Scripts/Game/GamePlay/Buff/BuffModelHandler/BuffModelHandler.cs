using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BuffModelHandler
    {
        public abstract void OnAddBuff(BattleRoleModel model);
        public abstract void OnRemoveBuff(BattleRoleModel model);
    }

    public abstract class BuffModelHandler_Status : BuffModelHandler
    {
        protected StatusControlType m_StatusControlType;
        protected bool m_IsOn;

        public BuffModelHandler_Status(StatusControlType statusControlType, bool isOn)
        {
            m_StatusControlType = statusControlType;
            m_IsOn = isOn;
        }
    }

    public abstract class BuffModelHandler_Attribute : BuffModelHandler
    {
        protected ModifyType m_ModifyType;
        protected int m_Value;

        public BuffModelHandler_Attribute(ModifyType modifyType, int value)
        {
            m_ModifyType = modifyType;
            m_Value = value;
        }

    }

}