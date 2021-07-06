using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDMonthCardConfigTable
    {
        public static MonthCardConfig monthCardConfig;
        static void CompleteRowAdd(TDMonthCardConfig tdData, int rowCount)
        {
            try
            {
                monthCardConfig = new MonthCardConfig(tdData);
            }
            catch (Exception e)
            {
                Log.e("e = {0} , row = {1} " + e, tdData.id);
            }
        }

        #region Public
        /// <summary>
        /// 获取月卡配置信息
        /// </summary>
        /// <returns></returns>
        public static MonthCardConfig GetMonthCardConfig()
        {
            return monthCardConfig;
        }
        #endregion
    }

    #region Struct
    /// <summary>
    /// 月卡配置表
    /// </summary>
    public struct MonthCardConfig
    {
        public int id;
        public float price;
        public int dailyDiamond;
        public int firstDiamond;
        public float renewPrice;

        public MonthCardConfig(TDMonthCardConfig tdData)
        {
            this.id = tdData.id;
            this.price = tdData.price;
            this.dailyDiamond = tdData.dailyDiamond;
            this.firstDiamond = tdData.firstDiamond;
            this.renewPrice = tdData.renewPrice;
        }
    }
    #endregion
}