﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public struct ShipUnitData
	{
        public ShipUnitType unitType;
        public int level;

        public ShipUnitData(ShipUnitType shipUnitType)
        {
            unitType = shipUnitType;
            level = 1;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
            level = Mathf.Max(0, level);
        }
	}
	
}