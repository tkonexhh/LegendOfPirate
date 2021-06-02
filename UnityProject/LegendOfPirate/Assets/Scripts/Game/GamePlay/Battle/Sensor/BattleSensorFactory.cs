using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensorFactory
    {
        public static IBattleSensor CreateBattleSensor(PickTargetType pickTargetType, SensorTypeEnum sensorType)
        {
            if (pickTargetType == PickTargetType.Self)
            {
                return new BattleSensor_Self();
            }

            switch (sensorType)
            {
                case SensorTypeEnum.HPLow:
                    return new BattleSensor_MinHp(pickTargetType);
                case SensorTypeEnum.HPHigh:
                    return new BattleSensor_MaxHp(pickTargetType);
                case SensorTypeEnum.HPRateHigh:
                case SensorTypeEnum.HPRateLow:
                case SensorTypeEnum.Farest:
                case SensorTypeEnum.Nearest:
                    return new BattleSensor_Nearest(pickTargetType);


            }
            return null;
        }
    }

}