using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityTrainingRoomTable
    {
        public static TrainingRoomUnitConfig[] trainingRoomUnitProperties = null;

        static void CompleteRowAdd(TDFacilityTrainingRoom tdData, int rowCount)
        {
            if (trainingRoomUnitProperties == null)
                trainingRoomUnitProperties = new TrainingRoomUnitConfig[rowCount];

            int level = tdData.level;
            if (level > trainingRoomUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("TrainingRoom Data Out Of Range");
            }

            trainingRoomUnitProperties[level - 1] = new TrainingRoomUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.modelResources, tdData.capacity, tdData.experience, tdData.trainingSpeed);
        }

        public static TrainingRoomUnitConfig GetConfig(int level)
        {
            if (level > trainingRoomUnitProperties.Length)
            {
                Log.e("GetTrainingRoomConfig Level Out Of Range: " + level + "  Data Count: " + trainingRoomUnitProperties.Length);
                return default(TrainingRoomUnitConfig);
            }

            return trainingRoomUnitProperties[level - 1];
        }
    }

    public struct TrainingRoomUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int capacity;
        public int experience;
        public int trainingTime;

        public TrainingRoomUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            string modelRes, int capacity, int experience, int trainingTime
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, modelRes);
            this.capacity = capacity;
            this.experience = experience;
            this.trainingTime = trainingTime;
        }
    }
}