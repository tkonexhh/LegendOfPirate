using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffFactory
    {

        public static Buff CreateBuff(int id, AttributeType attributeType, ModifyType modifyType, int value)
        {
            Buff buff = null;
            switch (attributeType)
            {
                case AttributeType.MoveSpeed:
                    buff = new Buff_MoveSpeed(id, modifyType, value);
                    break;
            }

            return buff;
        }

        public static Buff CreateBuff(BuffConfigSO configSO)
        {
            var buff = CreateBuff(configSO.ID, configSO.AttributeType, configSO.ModifyType, configSO.NumericValue);
            DealWithAppend(configSO);
            return buff;
        }




        #region Buff Append
        private static Dictionary<int, BuffAppendHandler> s_BuffAppendHandleMap = new Dictionary<int, BuffAppendHandler>();
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

        public static BuffAppendHandler GetAppendHandler(int id)
        {
            if (s_BuffAppendHandleMap.ContainsKey(id))
            {
                return s_BuffAppendHandleMap[id];
            }
            return null;
        }

        private static void DealWithAppend(BuffConfigSO configSO)
        {
            if (s_BuffAppendHandleMap.ContainsKey(configSO.ID))//已经处理过叠加了
            {
                return;
            }

            //处理叠加
            if (configSO.EnabledAppend)
            {
                var appendHandler = CreateAppendHandler(configSO.AppendType);
                if (appendHandler != null)
                {
                    s_BuffAppendHandleMap.Add(configSO.ID, appendHandler);
                }
            }
        }

        #endregion
    }

}