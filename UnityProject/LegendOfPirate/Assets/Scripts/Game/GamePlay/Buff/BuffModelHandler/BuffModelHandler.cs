using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BuffModelHandler
    {
        public abstract void OnAddBuff(BattleRoleRuntimeModel model);
        public abstract void OnRemoveBuff(BattleRoleRuntimeModel model);
    }

    public class BuffModelHandler_Status : BuffModelHandler
    {
        protected StatusControlType m_StatusControlType;

        public BuffModelHandler_Status(StatusControlType statusControlType)
        {
            m_StatusControlType = statusControlType;
        }

        public override void OnAddBuff(BattleRoleRuntimeModel model)
        {
            model.StatusMask.AddStatus(m_StatusControlType);
        }

        public override void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            model.StatusMask.RemoveStatus(m_StatusControlType);
        }
    }

    public abstract class BuffModelHandler_Attribute : BuffModelHandler
    {
        protected int m_Value;

        public BuffModelHandler_Attribute(int value)
        {
            m_Value = value;
        }

        public abstract void OnAppendBuff(int appendNum, BattleRoleRuntimeModel model);

    }

}