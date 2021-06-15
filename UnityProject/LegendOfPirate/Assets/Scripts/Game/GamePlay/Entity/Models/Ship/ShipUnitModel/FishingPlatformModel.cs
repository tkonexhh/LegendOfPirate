using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{

    public class FishingPlatformModel : ShipUnitModel
    {
        public FishingUnitConfig tableConfig;

        public FishingPlatformModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityFishingPlatformTable.GetConfig(level.Value);
        }
    }

}