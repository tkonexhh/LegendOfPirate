using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipUnitModelFactory
	{
        public static ShipUnitModel CreateUnitModel(ShipUnitData shipUnitData)
        {
            ShipUnitType shipUnitType = shipUnitData.unitType;
            ShipUnitModel unitModel = null;

            switch (shipUnitType)
            {
                case ShipUnitType.FishingPlatform:
                    unitModel = new FishingPlatformModel(shipUnitData);
                    break;
                case ShipUnitType.Garden:
                    unitModel = new GardenModel(shipUnitData);
                    break;
                case ShipUnitType.Kitchen:
                    unitModel = new KitchenModel(shipUnitData);
                    break;
                case ShipUnitType.Laboratory:
                    unitModel = new LaboratoryModel(shipUnitData);
                    break;
                case ShipUnitType.Library:
                    unitModel = new LibraryModel(shipUnitData);
                    break;
                case ShipUnitType.ProcessingRoom:
                    unitModel = new ProcessingRoomModel(shipUnitData);
                    break;
                case ShipUnitType.TrainingRoom:
                    unitModel = new TrainingRoomModel(shipUnitData);
                    break;

                case ShipUnitType.Forge:
                    unitModel = new ForgeModel(shipUnitData);
                    break;
            }

            return unitModel;
        }
	}
	
}