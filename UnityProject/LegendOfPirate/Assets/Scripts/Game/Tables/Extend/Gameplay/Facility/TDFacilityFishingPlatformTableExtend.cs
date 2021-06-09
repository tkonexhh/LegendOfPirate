using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityFishingPlatformTable
    {

        static void CompleteRowAdd(TDFacilityFishingPlatform tdData)
        {
            if(fishingUnitProperties == null)
                fishingUnitProperties = new FishingUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > fishingUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Fish Data Out Of Range");
            }

            fishingUnitProperties[level - 1] = new FishingUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeSpeed, tdData.modelResources, tdData.fishingRod, tdData.fishingSpeed,
                tdData.capability, tdData.unlockRecipe);
        }

        public static FishingUnitProperty[] fishingUnitProperties = null;
    }

    public struct FishingUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public string fishingRod;
        public int fishingTime;
        public UnlockFish[] unclockFishs;

        public FishingUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, string fishingRodRes, int fishingSpeed, int capability, string unlockFish
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            fishingRod = fishingRodRes;
            fishingTime = fishingSpeed;

            //Parse UnlockFish
            string[] unlockFishStrs = unlockFish.Split(';');
            if (unlockFishStrs.Length > 0)
            {
                this.unclockFishs = new UnlockFish[unlockFishStrs.Length];
                for (int i = 0; i < unlockFishStrs.Length; i++)
                {
                    string[] itemStr = unlockFishStrs[i].Split('|');
                    if (itemStr.Length == 2)
                    {
                        FishType fishType = (FishType)(int.Parse(itemStr[0]));
                        int count = int.Parse(itemStr[1]);

                        UnlockFish fish = new UnlockFish(fishType, count);
                        unclockFishs[i] = fish;
                    }
                    else
                    {
                        Log.e("Unlock Fish Pattern Wrong: " + unlockFishStrs[i]);
                    }
                }
            }
            else
            {
                this.unclockFishs = new UnlockFish[0];
                Log.e("Unlock Fish Pattern Wrong: " + unlockFish);
            }
        }
    }

    public struct UnlockFish
    {
        public FishType fishType;
        public int percent;

        public UnlockFish(FishType fishType, int percent)
        {
            this.fishType = fishType;
            this.percent = percent;
        }
    }
}