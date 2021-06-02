using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensorFactory
    {
        public static IBattleSensor CreateBattleSensor(SensorTypeEnum sensorType)
        {
            switch (sensorType)
            {
                case SensorTypeEnum.HPLow:
                    return new BattleSensor_MinHp();
                case SensorTypeEnum.HPHigh:
                    return new BattleSensor_MaxHp();
                case SensorTypeEnum.HPRateHigh:
                case SensorTypeEnum.HPRateLow:
                case SensorTypeEnum.Farest:
                case SensorTypeEnum.Nearest:
                    return new BattleSensor_Nearest();


            }
            return null;
        }
    }

}