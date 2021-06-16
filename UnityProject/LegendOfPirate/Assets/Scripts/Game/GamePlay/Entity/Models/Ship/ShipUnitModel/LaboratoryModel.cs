using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class LaboratoryModel : ShipUnitModel
    {
        public LaboratoryUnitConfig tableConfig;

        public LaboratoryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLaboratoryTable.GetConfig(level.Value);
        }
    }

}