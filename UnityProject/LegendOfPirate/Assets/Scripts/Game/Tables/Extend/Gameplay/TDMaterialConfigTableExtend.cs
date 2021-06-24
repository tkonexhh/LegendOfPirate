using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDMaterialConfigTable
    {
        public static MaterialUnitConfig[] matUnitProperites = null;
        private static int m_MatUnitIndex = 0;

        static void CompleteRowAdd(TDMaterialConfig tdData, int rowCount)
        {
            try
            {
                if (matUnitProperites == null)
                    matUnitProperites = new MaterialUnitConfig[rowCount];

                if (m_MatUnitIndex > matUnitProperites.Length)
                    throw new ArgumentOutOfRangeException("Material Data Out Of Range");

                matUnitProperites[m_MatUnitIndex] = new MaterialUnitConfig(tdData);
                m_MatUnitIndex++;
            }
            catch (Exception e)
            {
                Log.e("e =" + e);
            }
        }
        public static TDMaterialConfig GetConfigById(int id) 
        {
            foreach (var item in dataList) 
            {
                if (item.materialId == id) return item;
            }
            return null;
        }
    }


    #region Struct
    /// <summary>
    /// 材料结构体
    /// </summary>
    public struct MaterialUnitConfig
    {
        public int materialID;
        public string materialName;
        public string materialIcon;
        public string matDesc;
        public int sellingPrice;
        public MaterialQualityType matQualityType;
        public MaterialBaseType matBaseType;
        public MaterialOutputWay matOutputWay;

        public MaterialUnitConfig(TDMaterialConfig tdData)
        {
            this.materialID = tdData.materialId;
            this.materialName = tdData.materialName;
            this.materialIcon = tdData.materialIcon;
            this.matQualityType = EnumUtil.ConvertStringToEnum<MaterialQualityType>(tdData.quality.ToString());
            this.matDesc = tdData.materialDesc;
            this.matBaseType = EnumUtil.ConvertStringToEnum<MaterialBaseType>(tdData.materialType);
            this.sellingPrice = tdData.materialPrice;
            this.matOutputWay = (MaterialOutputWay)tdData.outputWay;
        }
    }
    #endregion
}