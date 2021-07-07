using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDBlackMarketConfigTable
    {
        public static BlackMarketConfig[] blackMarketProperties = null;
        private static int m_Index = 0;
        static void CompleteRowAdd(TDBlackMarketConfig tdData, int rowCount)
        {
            try
            {
                if (blackMarketProperties == null)
                    blackMarketProperties = new BlackMarketConfig[rowCount];
                blackMarketProperties[m_Index] = new BlackMarketConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.id);
                Log.e("e : " + e);
            }
        }

        public static BlackMarketConfig GetBlackMarketConfig(int id)
        {
            foreach (var item in blackMarketProperties)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
            Log.e("Error : Not find id = " + id);
            return default(BlackMarketConfig);
        }
    }

    #region Struct
    public struct MarketConfigItem
    {
        public int id;
        public int count;

        public MarketConfigItem(string str)
        {
            string[] strs = str.Split('|');
            this.id = int.Parse(strs[0]);
            this.count = int.Parse(strs[1]);
        }
    }

    public struct BlackMarketConfig
    {
        public int id;
        public string name;
        public int price;
        public MarketConfigItem marketConfigItem;
        public string iconName;

        public BlackMarketConfig(TDBlackMarketConfig tdData)
        {
            this.id = tdData.id;
            this.name = tdData.name;
            this.price = tdData.price;
            this.marketConfigItem = new MarketConfigItem(tdData.commodity);
            this.iconName = tdData.iconName;
        }
    }
    #endregion
}