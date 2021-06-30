using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDGlobalConfigTable
    {
        static void CompleteRowAdd(TDGlobalConfig tdData, int rowCount)
        {
        }

        public static float GetRoleAttributesGrowRateOfLevel() 
        {
            return m_DataList[0].roleAttributesGrowRateOfLevel;
        }

        public static float GetRoleAttributesGrowRateOfStar() 
        {
            return m_DataList[0].roleAttributesGrowRateOfStar;
        }
    }
}
