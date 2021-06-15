using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
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

}