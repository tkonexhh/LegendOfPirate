using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

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

            if (level.Value == GetMaxLevel())
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.COMMON_CONT_Ⅱ);
                return;
            }
           
            //TODO 最大等级待定
            if (level.Value + delta <= GetMaxLevel())
            {
                level.Value += delta;

                GameDataMgr.S.GetData<ShipData>().OnUnitUpgrade(unitType, delta);
            }
            else
            {
                delta = Define.TRAINING_ROOM_MAX_LEVEL - level.Value;
                OnLevelUpgrade( delta);
            }
        }

        /// <summary>
        /// 获得不同设别的最大等级
        /// </summary>
        /// <returns></returns>
        private int GetMaxLevel()
        {
            switch (unitType)
            {
                case ShipUnitType.None:
                    break;
                case ShipUnitType.Kitchen:
                    break;
                case ShipUnitType.FishingPlatform:
                    break;
                case ShipUnitType.Garden:
                    break;
                case ShipUnitType.Laboratory:
                    break;
                case ShipUnitType.Library:
                    break;
                case ShipUnitType.ProcessingRoom:
                    break;
                case ShipUnitType.TrainingRoom:
                    return TDFacilityTrainingRoomTable.GetMaxLevel();
                case ShipUnitType.ForgeRoom:
                    break;
            }
            return 9;
        }
    }
}