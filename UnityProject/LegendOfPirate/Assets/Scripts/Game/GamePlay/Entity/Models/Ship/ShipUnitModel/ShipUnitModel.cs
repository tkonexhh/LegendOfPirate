using System.Collections;
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

        public virtual void OnLevelUpgrade(int delta = 1)
        {
            if (delta == 0)
                return;
            //TODO 最大等级待定
            if (level.Value+delta<=Define.TRAINING_ROOM_MAX_LEVEL)
            {
                level.Value += delta;

                GameDataMgr.S.GetData<ShipData>().OnUnitUpgrade(unitType, delta);
            }
            else 
            {
                delta = Define.TRAINING_ROOM_MAX_LEVEL - level.Value;
                OnLevelUpgrade(delta);
            }
        }
    }
}