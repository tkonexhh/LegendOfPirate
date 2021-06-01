using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffFactory
    {
        public static Buff CreateBuff(BuffConfigSO configSO)
        {
            var buff = new Buff(configSO.ID);
            buff.time = configSO.Time;
            buff.AttributeHandler = CreateBuffModelHandlerAttribute(configSO.AttributeType, configSO.ModifyType, configSO.NumericValue);
            if (configSO.StatusControls != StatusControlType.None)//&& configSO.StatusControls.Count > 0)
            {
                buff.StatusHandler = new BuffModelHandler_Status(configSO.StatusControls);//new List<BuffModelHandler_Status>();
                // for (int i = 0; i < configSO.StatusControls.Count; i++)
                // {
                //     buff.StatusHandler.Add(CreateBuffModelHandlerStatus(configSO.StatusControls[i].statusType));
                // }
            }
            DealWithStatic(configSO);
            return buff;
        }

        #region BuffModelHandler
        private static BuffModelHandler_Attribute CreateBuffModelHandlerAttribute(AttributeType attributeType, ModifyType modifyType, float value)
        {
            switch (attributeType)
            {
                case AttributeType.MoveSpeed:
                    return new BuffModelHandler_MoveSpeed(modifyType, value);
                case AttributeType.ATK:
                    return new BuffModelHandler_ATK(modifyType, value);
                case AttributeType.MaxHp:
                    return new BuffModelHandler_MaxHp(modifyType, value);
            }
            return null;
        }

        private static BuffModelHandler_Status CreateBuffModelHandlerStatus(StatusControlType statusType)
        {
            switch (statusType)
            {
                case StatusControlType.AttackForbid:
                    return new BuffModelHandler_AttackForbid();
                case StatusControlType.MoveForbid:
                    return new BuffModelHandler_MoveForbid();
                case StatusControlType.SkillForbid:
                    return new BuffModelHandler_SkillForbid();
            }
            return null;
        }
        #endregion

        #region Buff Append
        private static Dictionary<int, BuffStaticInfo> s_BuffStaticMap = new Dictionary<int, BuffStaticInfo>();
        private static BuffAppendHandler CreateAppendHandler(BuffAppendType appendType)
        {
            switch (appendType)
            {
                case BuffAppendType.Additive:
                    return new BuffAppendHandler_Additive();
                case BuffAppendType.Continue:
                    return new BuffAppendHandler_Continue();
                case BuffAppendType.Reset:
                    return new BuffAppendHandler_Reset();
            }
            return null;
        }

        public static BuffStaticInfo GetBuffStaticInfo(int id)
        {
            if (s_BuffStaticMap.ContainsKey(id))
            {
                return s_BuffStaticMap[id];
            }
            return null;
        }

        private static void DealWithStatic(BuffConfigSO configSO)
        {
            if (s_BuffStaticMap.ContainsKey(configSO.ID))//已经处理过叠加了
            {
                return;
            }
            BuffStaticInfo staticInfo = new BuffStaticInfo();
            staticInfo.maxAppendNum = configSO.MaxAppendNum;

            //处理叠加
            if (configSO.EnabledAppend)
            {
                var appendHandler = CreateAppendHandler(configSO.AppendType);
                if (appendHandler != null)
                {
                    staticInfo.appendHandler = appendHandler;

                }
            }

            s_BuffStaticMap.Add(configSO.ID, staticInfo);
        }

        #endregion
    }


    public class BuffStaticInfo
    {
        public int maxAppendNum;
        public BuffAppendHandler appendHandler;
    }

}