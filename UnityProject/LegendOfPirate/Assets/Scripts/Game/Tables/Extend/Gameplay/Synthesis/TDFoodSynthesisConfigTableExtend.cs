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
        static void CompleteRowAdd(TDFoodSynthesisConfig tdData, int rowCount)
        {

        }
    }
    #region Struct

    public struct MakeCostRes
    {
        public int matID;
        public int number;
    }

    public struct FoodSynthesisUnitConfig
    {
        public int ID;
        public string foodName;
        public string spriteName;
        //public  Quality
        public string desc;
        public MakeCostRes[] makeCostRes;
        //public BuffType
        //public BuffRate
        public int buffTime;
        public int makeTime;
        public int costCoin;
    }
    #endregion
}