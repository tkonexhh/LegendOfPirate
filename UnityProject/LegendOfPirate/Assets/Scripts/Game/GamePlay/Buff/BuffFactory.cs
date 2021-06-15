using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffFactory
    {
        public static Buff CreateBuff(BuffConfigSO configSO)
        {
            var buff = new Buff();
            buff.id = configSO.ID;
            buff.time = configSO.Time;
            buff.BuffTrigger = BuffTriggerFactory.CreateBuffTrigger(configSO.BuffTriggerType);
            if (configSO.EnabledAttributeModify)
            {
                for (int i = 0; i < configSO.ModifierAttributeLst.Count; i++)
                {
                    if (buff.AttributeHandler == null)
                    {
                        buff.AttributeHandler = new List<BuffModelHandler_Attribute>();
                    }
                    buff.AttributeHandler.Add(CreateBuffModelHandlerAttribute(configSO.ModifierAttributeLst[i].AttributeType, configSO.ModifierAttributeLst[i].NumericValue));
                }
            }

            if (configSO.EnabledStateModify)
            {
                if (configSO.StatusControls != StatusControlType.None)
                {
                    buff.StatusHandler = new BuffModelHandler_Status(configSO.StatusControls);
                }
            }

            DealWithStatic(configSO);
            return buff;
        }

        #region BuffModelHandler
        private static BuffModelHandler_Attribute CreateBuffModelHandlerAttribute(AttributeType attributeType, int value)
        {
            switch (attributeType)
            {
                case AttributeType.MoveSpeed:
                    return new BuffModelHandler_MoveSpeed(value);
                case AttributeType.ATK:
                    return new BuffModelHandler_ATK(value);
                case AttributeType.MaxHp:
                    return new BuffModelHandler_MaxHp(value);
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

            //处理叠加
            if (configSO.EnabledAppend)
            {
                staticInfo.maxAppendNum = configSO.MaxAppendNum;
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