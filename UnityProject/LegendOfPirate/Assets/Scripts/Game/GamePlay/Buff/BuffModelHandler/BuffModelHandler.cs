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

    public class BuffModelHandler_Status : BuffModelHandler
    {
        protected StatusControlType m_StatusControlType;

        public BuffModelHandler_Status(StatusControlType statusControlType)
        {
            m_StatusControlType = statusControlType;
        }

        public override void OnAddBuff(BattleRoleModel model)
        {
            model.StatusMask.AddStatus(m_StatusControlType);
        }

        public override void OnRemoveBuff(BattleRoleModel model)
        {
            model.StatusMask.RemoveStatus(m_StatusControlType);
        }
    }

    public abstract class BuffModelHandler_Attribute : BuffModelHandler
    {
        protected ModifyType m_ModifyType;
        protected float m_Value;

        public BuffModelHandler_Attribute(ModifyType modifyType, float value)
        {
            m_ModifyType = modifyType;
            m_Value = value;
        }

        public abstract void OnAppendBuff(int appendNum, BattleRoleModel model);

    }

}