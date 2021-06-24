using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPotionSynthesisConfigTable
    {
        static void CompleteRowAdd(TDPotionSynthesisConfig tdData, int rowCount)
        {

        }

        public static TDPotionSynthesisConfig GetConfigById(int id) 
        {
            foreach (var item in dataList) 
            {
                if (item.id == id) return item;
            }
            return null;
        }
    }
}