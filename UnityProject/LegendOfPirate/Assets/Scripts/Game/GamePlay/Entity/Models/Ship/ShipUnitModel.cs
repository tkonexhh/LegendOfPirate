using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    // 船的组件Model
	public class ShipUnitModel : Model
	{
        public ShipUnitType unitType;
        public IntReactiveProperty level;

        public ShipUnitModel(ShipUnitData shipUnitData)
        {
            unitType = shipUnitData.unitType;
            level = new IntReactiveProperty(shipUnitData.level);
        }
    }

    public class KitchenModel : ShipUnitModel
    {
        public KitchenUnitConfig tableConfig;
        public KitchenData kitchenDbData;

        public KitchenModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityKitchenTable.GetConfig(level.Value);
            kitchenDbData = GameDataMgr.S.GetData<KitchenData>();
        }
    }

    public class FishingPlatformModel : ShipUnitModel
    {
        public FishingUnitConfig tableConfig;

        public FishingPlatformModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityFishingPlatformTable.GetConfig(level.Value);
        }
    }

    public class GardenModel : ShipUnitModel
    {
        public GardenUnitConfig tableConfig;

        public GardenModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityGardenTable.GetConfig(level.Value);
        }
    }

    public class LaboratoryModel : ShipUnitModel
    {
        public LaboratoryUnitConfig tableConfig;

        public LaboratoryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLaboratoryTable.GetConfig(level.Value);
        }
    }

    public class LibraryModel : ShipUnitModel
    {
        public LibraryUnitConfig tableConfig;

        public LibraryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLibraryTable.GetConfig(level.Value);
        }
    }

    public class ProcessingRoomModel : ShipUnitModel
    {
        public ProcessingRoomUnitConfig tableConfig;

        public ProcessingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityProcessingRoomTable.GetConfig(level.Value);
        }
    }

    public class TrainingRoomModel : ShipUnitModel
    {
        public TrainingRoomUnitConfig tableConfig;

        public TrainingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);
        }
    }

    public class ForgeRoomModel : ShipUnitModel
    {
        public ForgeUnitConfig tableConfig;

        public ForgeRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityForgeTable.GetConfig(level.Value);
        }
    }
}