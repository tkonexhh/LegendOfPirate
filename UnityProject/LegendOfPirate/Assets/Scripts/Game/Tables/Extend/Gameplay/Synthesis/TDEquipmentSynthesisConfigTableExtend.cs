using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDEquipmentSynthesisConfigTable
    {
        static void CompleteRowAdd(TDEquipmentSynthesisConfig tdData, int rowCount)
        {

        }

        public static TDEquipmentSynthesisConfig GetEquipmentSynthesisById(int equipId) 
        {
            foreach (TDEquipmentSynthesisConfig item in dataList) 
            {
                if (item.id == equipId) return item;
            }
            return default(TDEquipmentSynthesisConfig);
        }
      
    }
}