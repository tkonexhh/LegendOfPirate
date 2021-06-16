using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class ProcessingRoomModel : ShipUnitModel
    {
        public ProcessingRoomUnitConfig tableConfig;

        public ProcessingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityProcessingRoomTable.GetConfig(level.Value);
        }
    }

}