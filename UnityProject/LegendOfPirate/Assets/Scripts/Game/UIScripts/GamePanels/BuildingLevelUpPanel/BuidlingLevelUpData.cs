using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class BuildingLevelUpData 
	{
		public string buildingName;
		public List<int> needElementIds;
		public List<int> needElementCounts;
		public string[] buildingImgs;
        public BuildingLevelUpData() 
        {
            needElementCounts = new List<int>();
            needElementIds = new List<int>();
        }
	}
	public class BuildingLevelUpDataFactory : TSingleton<BuildingLevelUpDataFactory> 
	{
		public BuildingLevelUpData GetBuildingLevelUpData(ShipUnitType unittype) 
		{
            BuildingLevelUpData ret = new BuildingLevelUpData();
            ret.buildingName = unittype.ToString();
            ShipModel ship_model = ModelMgr.S.GetModel<ShipModel>();
            switch (unittype)
            {
                case ShipUnitType.Kitchen:
                    KitchenModel kitchen_model = ship_model.GetShipUnitModel(ShipUnitType.Kitchen)as KitchenModel;
                    ret.buildingName = "Kitchen";
                    foreach (var item in kitchen_model.tableConfig.baseProperty.upgradeResCosts) 
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.FishingPlatform:
                   FishingPlatformModel fishing_model = ship_model.GetShipUnitModel(ShipUnitType.FishingPlatform) as FishingPlatformModel;
                    ret.buildingName = "FishingPlatform";
                    foreach (var item in fishing_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.Garden:
                    GardenModel garden_model = ship_model.GetShipUnitModel(ShipUnitType.Garden) as GardenModel;
                    ret.buildingName = "Garden";
                    foreach (var item in garden_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.Laboratory:
                    LaboratoryModel laboratory_model = ship_model.GetShipUnitModel(ShipUnitType.Laboratory) as LaboratoryModel;
                    ret.buildingName = "Laboratory";
                    foreach (var item in laboratory_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.Library:
                    LibraryModel library_model = ship_model.GetShipUnitModel(ShipUnitType.Library) as LibraryModel;
                    ret.buildingName = "Library";
                    foreach (var item in library_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.ProcessingRoom:
                    ProcessingRoomModel prossingroom_model = ship_model.GetShipUnitModel(ShipUnitType.ProcessingRoom) as ProcessingRoomModel;
                    ret.buildingName = "ProcessingRoom";
                    foreach (var item in prossingroom_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.TrainingRoom:
                    TrainingRoomModel trainingroom_model = ship_model.GetShipUnitModel(ShipUnitType.TrainingRoom) as TrainingRoomModel;
                    ret.buildingName = "TrainingRoom";
                    foreach (var item in trainingroom_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
                case ShipUnitType.ForgeRoom:
                    ForgeRoomModel forgeroom_model = ship_model.GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
                    ret.buildingName = "ForgeRoom";
                    foreach (var item in forgeroom_model.tableConfig.baseProperty.upgradeResCosts)
                    {
                        ret.needElementCounts.Add(item.count);
                        ret.needElementIds.Add((int)item.rawMatType);
                    }
                    break;
            }

            return ret;
        }
    }

}