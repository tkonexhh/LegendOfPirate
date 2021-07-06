using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System.Linq;
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
	public class BuildingLevelUpDataFactory 
	{
		public static BuildingLevelUpData GetBuildingLevelUpData(ShipUnitType unittype) 
		{
            BuildingLevelUpData ret = new BuildingLevelUpData();
            ret.buildingName = unittype.ToString();
            ShipModel ship_model = ModelMgr.S.GetModel<ShipModel>();
            switch (unittype)
            {
                case ShipUnitType.Kitchen:

                    KitchenModel kitchen_model = ship_model.GetShipUnitModel(ShipUnitType.Kitchen)as KitchenModel;
                    
                    ret.buildingName = "Kitchen";
                    
                    var kitchenconfig=  TDFacilityKitchenTable.dataList.FirstOrDefault(data => data.level == kitchen_model.level.Value);
                    
                    foreach (var item in kitchenconfig.GetUpGradeCostRes()) 
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add(item.resId);
                    }

                    break;

                case ShipUnitType.FishingPlatform:
                   
                    FishingPlatformModel fishing_model = ship_model.GetShipUnitModel(ShipUnitType.FishingPlatform) as FishingPlatformModel;
                    
                    ret.buildingName = "FishingPlatform";
                   
                    var fishingconfig = TDFacilityFishingPlatformTable.dataList.FirstOrDefault(data => data.level == fishing_model.level.Value);
                    
                    foreach (var item in fishingconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add((int)item.resId);
                    }
                    
                    break;

                case ShipUnitType.Garden:
                    
                    GardenModel garden_model = ship_model.GetShipUnitModel(ShipUnitType.Garden) as GardenModel;
                    
                    ret.buildingName = "Garden";

                    var gardenconfig = TDFacilityGardenTable.dataList.FirstOrDefault(data => data.level == garden_model.level.Value);

                    foreach (var item in gardenconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add(item.resId);
                    }

                    break;

                case ShipUnitType.Laboratory:

                    LaboratoryModel laboratory_model = ship_model.GetShipUnitModel(ShipUnitType.Laboratory) as LaboratoryModel;

                    ret.buildingName = "Laboratory";

                    var laboratoryconfig = TDFacilityLaboratoryTable.dataList.FirstOrDefault(data => data.level == laboratory_model.level.Value);

                    foreach (var item in laboratoryconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add(item.resId);
                    }

                    break;

                case ShipUnitType.Library:

                    LibraryModel library_model = ship_model.GetShipUnitModel(ShipUnitType.Library) as LibraryModel;
                    
                    ret.buildingName = "Library";

                    var libraryconfig = TDFacilityLibraryTable.dataList.FirstOrDefault(data => data.level == library_model.level.Value);

                    foreach (var item in libraryconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add(item.resId);
                    }

                    break;
 
                case ShipUnitType.ProcessingRoom:
                   
                    ProcessingRoomModel prossingroom_model = ship_model.GetShipUnitModel(ShipUnitType.ProcessingRoom) as ProcessingRoomModel;
                    
                    ret.buildingName = "ProcessingRoom";

                    var prossingroomconfig = TDFacilityProcessingRoomTable.dataList.FirstOrDefault(data => data.level == prossingroom_model.level.Value);
                    
                    foreach (var item in prossingroomconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add((int)item.resId);
                    }

                    break;

                case ShipUnitType.TrainingRoom:

                    TrainingRoomModel trainingroom_model = ship_model.GetShipUnitModel(ShipUnitType.TrainingRoom) as TrainingRoomModel;
                    
                    ret.buildingName = "TrainingRoom";

                    var trainingroomconfig = TDFacilityTrainingRoomTable.dataList.FirstOrDefault(data => data.level == trainingroom_model.level.Value);
                    
                    foreach (var item in trainingroomconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add((int)item.resId);
                    }

                    break;
                
                case ShipUnitType.ForgeRoom:
                    
                    ForgeRoomModel forgeroom_model = ship_model.GetShipUnitModel(ShipUnitType.ForgeRoom) as ForgeRoomModel;
                    
                    ret.buildingName = "ForgeRoom";

                    var forgeroomconfig = TDFacilityForgeTable.dataList.FirstOrDefault(data => data.level == forgeroom_model.level.Value);
                    
                    foreach (var item in forgeroomconfig.GetUpGradeCostRes())
                    {
                        ret.needElementCounts.Add(item.resCount);
                        ret.needElementIds.Add((int)item.resId);
                    }

                    break;
            }

            return ret;
        }
    }

}