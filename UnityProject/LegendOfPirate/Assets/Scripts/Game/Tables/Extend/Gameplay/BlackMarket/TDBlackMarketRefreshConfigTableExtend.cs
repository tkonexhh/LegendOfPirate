using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDBlackMarketRefreshConfigTable
    {
        public static BlackMarketRefreshConfig[] marketRefreshProperties = null;
        private static int m_Index = 0;

        static void CompleteRowAdd(TDBlackMarketRefreshConfig tdData, int rowCount)
        {
            try
            {
                if (marketRefreshProperties == null)
                    marketRefreshProperties = new BlackMarketRefreshConfig[rowCount];
                marketRefreshProperties[m_Index] = new BlackMarketRefreshConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.id);
                Log.e("e : " + e);
            }
        }

        public static int GetMaxRefreshID()
        {
            
            return marketRefreshProperties[marketRefreshProperties.Length - 1].id;
        }

        public static BlackMarketRefreshConfig GetBlackMarketConfig(int id)
        {

            foreach (var item in marketRefreshProperties)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            Log.e("Error : Not find id = " + id);
            return default(BlackMarketRefreshConfig);
        }
    }

    #region Struct
    public struct BlackMarketRefreshConfig
    {
        public int id;
        public int cost;

        public BlackMarketRefreshConfig(TDBlackMarketRefreshConfig tdData)
        {
            this.id = tdData.id;
            this.cost = tdData.cost;
        }
    }
    #endregion
}