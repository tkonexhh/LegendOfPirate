using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class GardenModel : ShipUnitModel
    {
        public GardenUnitConfig tableConfig;

        public GardenModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityGardenTable.GetConfig(level.Value);
        }
    }
    public class GardenSlotModel : Model
    {
    
    }
    public class PlantSlotModel : Model 
    {
    
    }

}