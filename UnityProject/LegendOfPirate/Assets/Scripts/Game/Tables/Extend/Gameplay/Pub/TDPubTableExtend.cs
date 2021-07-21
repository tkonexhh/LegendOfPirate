using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPubTable
    {
        public static PubConfig[] PubConfigs = null;
        private static int m_Index = 0;

        static void CompleteRowAdd(TDPub tdData, int rowCount)
        {
            try
            {
                if (PubConfigs == null)
                    PubConfigs = new PubConfig[rowCount];
                PubConfigs[m_Index] = new PubConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.iD);
                Log.e("e : " + e);
            }
        }
    }

    #region Struct
    /// <summary>
    /// 抽奖物品类型
    /// </summary>
    public enum LotteryItemType
    {
        None,
        Hero,
        Equip,
        Material,
    }

    public struct PubItemDetails
    {
        public int id;
        public LotteryItemType lotteryItemType;
        public int number;
        public int weight;

        public PubItemDetails(string str)
        {
            string[] strs = str.Split('|');
            this.id = int.Parse(strs[0]);
            this.lotteryItemType = EnumUtil.ConvertStringToEnum<LotteryItemType>(strs[1]);
            this.number = int.Parse(strs[2]);
            this.weight = int.Parse(strs[3]);
        }
    }

    public struct PubConfig
    {
        public int id;
        public string itemName;
        public PubItemDetails pubItemDetails;
        public string IconName;
        public PubConfig(TDPub tdData)
        {
            this.id = tdData.iD;
            this.itemName = tdData.upgradeRes;
            this.pubItemDetails = new PubItemDetails(tdData.itemDetails);
            this.IconName = tdData.iconResources;
        }
    }
    #endregion
}