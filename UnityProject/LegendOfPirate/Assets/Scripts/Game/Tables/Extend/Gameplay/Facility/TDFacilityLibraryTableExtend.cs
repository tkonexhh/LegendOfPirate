using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityLibraryTable
    {
        static void CompleteRowAdd(TDFacilityLibrary tdData, int rowCount)
        {
            if (libraryUnitProperties == null)
                libraryUnitProperties = new LibraryUnitConfig[rowCount];

            int level = tdData.level;
            if (level > libraryUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Library Data Out Of Range");
            }

            libraryUnitProperties[level - 1] = new LibraryUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeSpeed, tdData.modelResources, tdData.capacity, tdData.skillPoints, tdData.readingSpeed);
        }

        public static LibraryUnitConfig[] libraryUnitProperties = null;

        public static LibraryUnitConfig GetConfig(int level)
        {
            if (level > libraryUnitProperties.Length)
            {
                Log.e("GetLibraryUnitProperty Level Out Of Range: " + level + "  Data Count: " + libraryUnitProperties.Length);
                return default(LibraryUnitConfig);
            }

            return libraryUnitProperties[level - 1];
        }
    }

    public struct LibraryUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int capacity;
        public int skillPoints;
        public int readingSpeed;

        public LibraryUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int capacity, int skillPoints, int readingSpeed
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.capacity = capacity;
            this.skillPoints = skillPoints;
            this.readingSpeed = readingSpeed;
        }
    }
}