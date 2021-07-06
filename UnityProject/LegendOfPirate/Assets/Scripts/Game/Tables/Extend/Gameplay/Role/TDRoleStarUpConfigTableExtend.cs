using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using System.Linq;
namespace GameWish.Game
{
    public partial class TDRoleStarUpConfigTable
    {
        static void CompleteRowAdd(TDRoleStarUpConfig tdData, int rowCount)
        {

        }

        public static int GetLevelUpNeedCount(int roleleve = 0) 
        {
            return m_DataList.FirstOrDefault(data =>( data.starLevel == roleleve + 1)).starUpCost;
        }

        //public static TDRoleStarUpConfig GetConfig(int level) 
        //{
        //    return dataList[level-1];
        //}
    }
}