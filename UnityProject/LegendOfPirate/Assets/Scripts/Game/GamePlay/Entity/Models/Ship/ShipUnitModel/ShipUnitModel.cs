﻿using System.Collections;
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

        public virtual void OnUpgrade(int delta)
        {
            level.First(x => x + delta <= Define.TRAINING_ROOM_MAX_SLOT).Subscribe(_ => {
                level.Value += delta;
            });

            GameDataMgr.S.GetData<ShipData>().OnUnitUpgrade(unitType,delta);
        }
    }
}