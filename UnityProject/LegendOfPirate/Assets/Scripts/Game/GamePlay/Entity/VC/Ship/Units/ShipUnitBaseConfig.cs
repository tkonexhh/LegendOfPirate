using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public struct ShipUnitBaseConfig
	{
        public int level;
        public int upgradeCoinCost;
        public UpgradeResCost[] upgradeResCosts;
        public int upgradePrecondition;
        public int upgradeTime;
        public string modelRes;

        public ShipUnitBaseConfig(int level, int upgradeCoinCost, string upgradeCost, int upgradePrecondition, int upgradeTime, string modelRes)
        {
            this.level = level;
            this.upgradeCoinCost = upgradeCoinCost;
            this.upgradePrecondition = upgradePrecondition;
            this.upgradeTime = upgradeTime;
            this.modelRes = modelRes;

            string[] costItmeStrs = upgradeCost.Split(';');
            if (costItmeStrs.Length > 0)
            {
                this.upgradeResCosts = new UpgradeResCost[costItmeStrs.Length];
                for (int i = 0; i < costItmeStrs.Length; i++)
                {
                    string[] itemStr = costItmeStrs[i].Split('|');
                    if (itemStr.Length == 2)
                    {
                        RawMatType rawMatType = (RawMatType)(int.Parse(itemStr[0]));
                        int count = int.Parse(itemStr[1]);

                        UpgradeResCost cost = new UpgradeResCost(rawMatType, count);
                        upgradeResCosts[i] = cost;
                    }
                    else
                    {
                        Log.e("Upgrade Cost Pattern Wrong: " + upgradeCost);
                    }
                }
            }
            else
            {
                this.upgradeResCosts = new UpgradeResCost[0];
                Log.e("Upgrade Cost Pattern Wrong: " + upgradeCost);
            }
        }
    }

    public struct UpgradeResCost
    {
        public RawMatType rawMatType;
        public int count;

        public UpgradeResCost(RawMatType rawMatType, int count)
        {
            this.rawMatType = rawMatType;
            this.count = count;
        }
    }
}