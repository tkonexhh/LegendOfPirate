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
            else if (pickTargetType == PickTargetType.Target)
            {
                return new BattleSensor_Target();
            }

            switch (sensorType)
            {
                case SensorTypeEnum.HPLow:
                    return new BattleSensor_MinHp(pickTargetType);
                case SensorTypeEnum.HPHigh:
                    return new BattleSensor_MaxHp(pickTargetType);
                case SensorTypeEnum.HPRateHigh:
                    return new BattleSensor_MaxHpRate(pickTargetType);
                case SensorTypeEnum.HPRateLow:
                    return new BattleSensor_MinHpRate(pickTargetType);
                case SensorTypeEnum.Farest:
                    return new BattleSensor_Farest(pickTargetType);
                case SensorTypeEnum.Nearest:
                    return new BattleSensor_Nearest(pickTargetType);
            }
            return null;
        }
    }

}