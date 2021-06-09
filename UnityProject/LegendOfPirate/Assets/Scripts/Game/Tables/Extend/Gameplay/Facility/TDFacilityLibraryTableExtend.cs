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
        static void CompleteRowAdd(TDFacilityLibrary tdData)
        {
            if (libraryUnitProperties == null)
                libraryUnitProperties = new LibraryUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > libraryUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Library Data Out Of Range");
            }

            libraryUnitProperties[level - 1] = new LibraryUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeSpeed, tdData.modelResources, tdData.capacity, tdData.skillPoints, tdData.readingSpeed);
        }

        public static LibraryUnitProperty[] libraryUnitProperties = null;

        public static LibraryUnitProperty GetLibraryUnitProperty(int level)
        {
            if (level > libraryUnitProperties.Length)
            {
                Log.e("GetLibraryUnitProperty Level Out Of Range: " + level + "  Data Count: " + libraryUnitProperties.Length);
                return default(LibraryUnitProperty);
            }

            return libraryUnitProperties[level - 1];
        }
    }

    public struct LibraryUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public int capacity;
        public int skillPoints;
        public int readingSpeed;

        public LibraryUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int capacity, int skillPoints, int readingSpeed
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.capacity = capacity;
            this.skillPoints = skillPoints;
            this.readingSpeed = readingSpeed;
        }
    }
}