using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{

    public class LibraryModel : ShipUnitModel
    {
        public LibraryUnitConfig tableConfig;

        public LibraryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLibraryTable.GetConfig(level.Value);
        }
    }

}