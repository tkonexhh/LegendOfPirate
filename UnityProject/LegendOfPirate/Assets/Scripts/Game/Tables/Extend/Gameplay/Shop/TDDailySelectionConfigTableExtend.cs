﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDDailySelectionConfigTable
    {
        public static DailySelectionConfig[] dailySelectionProperties = null;
        private static int m_Index = 0;
        static void CompleteRowAdd(TDDailySelectionConfig tdData, int rowCount)
        {
            try
            {
                if (dailySelectionProperties == null)
                    dailySelectionProperties = new DailySelectionConfig[rowCount];
                dailySelectionProperties[m_Index] = new DailySelectionConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.id);
                Log.e("e : " + e);
            }
        }
    }

    #region Struce
    public struct DailyConfigItem
    {
        public int id;
        public int count;

        public DailyConfigItem(string str)
        {
            string[] strs = str.Split('|');
            this.id = int.Parse(strs[0]);
            this.count = int.Parse(strs[1]);
        }
    }

    public struct DailySelectionConfig
    {
        public int id;
        public string itemName;
        public float price;
        public DailyConfigItem dailySelectionItem;
        public string iconName;

        public DailySelectionConfig(TDDailySelectionConfig tdData)
        {
            this.id = tdData.id;
            this.itemName = tdData.name;
            this.price = tdData.price;
            this.dailySelectionItem = new DailyConfigItem(tdData.content);
            this.iconName = tdData.iconName;
        }
    }
    #endregion
}