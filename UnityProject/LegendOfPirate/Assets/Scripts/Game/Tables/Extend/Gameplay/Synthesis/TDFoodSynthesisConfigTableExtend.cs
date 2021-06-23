using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFoodSynthesisConfigTable
    {
        public static FoodSynthesisUnitConfig[] foodUnitProperites = null;
        private static int m_FoodUnitIndex = 0;
        static void CompleteRowAdd(TDFoodSynthesisConfig tdData, int rowCount)
        {
            try
            {
                if (foodUnitProperites == null)
                    foodUnitProperites = new FoodSynthesisUnitConfig[rowCount];

                if (m_FoodUnitIndex > foodUnitProperites.Length)
                    throw new ArgumentOutOfRangeException("Food Data Out Of Range");

                foodUnitProperites[m_FoodUnitIndex] = new FoodSynthesisUnitConfig(tdData);
                m_FoodUnitIndex++;
            }
            catch (Exception e)
            {
                Log.e("e =" + e);
            }
        }

        public static TDFoodSynthesisConfig GetConfigById(int foodId) 
        {
            foreach (var item in dataList) 
            {
                if (item.id == foodId) return item;
            }
            return null;
        }
    }
    #region Struct
    public struct MakeCostRes
    {
        public int matID;
        public int number;
        public MakeCostRes(string strs)
        {
            string[] subStrs = strs.Split('|');
            this.matID = int.Parse(subStrs[0]);
            this.number = int.Parse(subStrs[1]);
        }
    }

    public struct FoodSynthesisUnitConfig
    {
        public int ID;
        public string foodName;
        public string spriteName;
        public FoodQualityType foodQualityType;
        public string desc;
        public MakeCostRes[] makeCostRes;
        public FoodBuffType foodBuffType;
        public float buffRate;
        public int buffTime;
        public int makeTime;
        public int costCoin;

        public FoodSynthesisUnitConfig(TDFoodSynthesisConfig tdData)
        {
            this.ID = tdData.id;
            this.foodName = tdData.name;
            this.spriteName = tdData.spriteName;
            this.foodQualityType = EnumUtil.ConvertStringToEnum<FoodQualityType>(tdData.quality);
            this.desc = tdData.desc;
            this.foodBuffType = EnumUtil.ConvertStringToEnum<FoodBuffType>(tdData.buffType);
            this.buffRate = float.Parse(tdData.buffRate);
            this.buffTime = tdData.buffTime;
            this.makeTime = tdData.makeTime;
            this.costCoin = tdData.makeCost;
            #region Analysis makeCostRes
            if (!string.IsNullOrEmpty(tdData.makeRes))
            {
                string[] strs = tdData.makeRes.Split(';');
                makeCostRes = new MakeCostRes[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    MakeCostRes cost = new MakeCostRes(strs[i]);
                    makeCostRes[i] = cost;
                }
            }
            else
            {
                makeCostRes = new MakeCostRes[0];
            }
            #endregion
        }
    }
    #endregion
}