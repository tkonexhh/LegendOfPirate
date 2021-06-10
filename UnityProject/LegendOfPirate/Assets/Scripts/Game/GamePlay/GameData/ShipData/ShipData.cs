using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{   
    public class ShipData : IDataClass
    {
        public int shipLevel;

        public List<ShipUnitData> shipUnitDataList = new List<ShipUnitData>();

        #region IDataClass

        public override void InitWithEmptyData()
        {
            foreach (var item in EnumUtil.GetValues<ShipUnitType>())
            {
                if (item != ShipUnitType.None)
                {
                    shipUnitDataList.Add(new ShipUnitData(item));
                }
            }
        }

        public override void OnDataLoadFinish()
        {

        }

        #endregion

        #region Public Set

        /// <summary>
        /// 当一个组件解锁时调用
        /// </summary>
        /// <param name="shipUnitType"></param>
        public void OnUnitUnlocked(ShipUnitType shipUnitType)
        {
            ShipUnitData? unitData = GetUnitData(shipUnitType);
            if (unitData != null)
            {
                Log.e("Ship Unit Already Exists : " + shipUnitType.ToString());
            }
            else
            {
                ShipUnitData data = new ShipUnitData(shipUnitType);
                shipUnitDataList.Add(data);
            }
        }

        /// <summary>
        /// 当一个组件升级时调用
        /// </summary>
        /// <param name="shipUnitType"></param>
        /// <param name="deltaLevel"></param>
        public void OnUnitUpgrade(ShipUnitType shipUnitType, int deltaLevel)
        {
            ShipUnitData? unitData = GetUnitData(shipUnitType);
            if (unitData == null)
            {
                Log.e("Ship Unit Not Found : " + shipUnitType.ToString());
            }
            else
            {
                unitData.Value.Upgrade(deltaLevel);
            }
        }

        #endregion

        #region Public Get

        /// <summary>
        /// 获取某个组件的数据
        /// </summary>
        /// <param name="shipUnitType"></param>
        /// <returns></returns>
        public ShipUnitData? GetUnitData(ShipUnitType shipUnitType)
        {
            return shipUnitDataList.FirstOrDefault(i => i.unitType == shipUnitType);
        }

        #endregion

        #region Private

        #endregion
    }
}