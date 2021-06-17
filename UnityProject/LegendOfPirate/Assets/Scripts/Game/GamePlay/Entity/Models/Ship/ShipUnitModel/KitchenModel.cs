using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public class KitchenSlotModel : Model
    {
        public int slotId;
        public int foodId;
        public DateTime startTime=default(DateTime);
        public TimeSpan timeSpan=default(TimeSpan);
        public KitchenModel kitchenModel;
        
        public KitchenSlotModel(KitchenModel kitchenmodel)
        {
            kitchenModel = kitchenmodel;
        }
    }
}