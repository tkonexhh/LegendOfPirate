using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class ForgeRoomModel : ShipUnitModel
    {
        public ForgeUnitConfig tableConfig;

        public ForgeRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityForgeTable.GetConfig(level.Value);
        }
    }

}