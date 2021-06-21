using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPartSynthesisConfigTable
    {
        static void CompleteRowAdd(TDPartSynthesisConfig tdData, int rowCount)
        {

        }
        public static TDPartSynthesisConfig GetConfigById(int partId) 
        {
            foreach (var item in dataList) 
            {
                if (item.id == partId) return item;
            }
            return null;
        }
    }
}