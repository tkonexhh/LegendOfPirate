using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public class RoleEquipData
    {
        public int id;
        public int level;
        public EquipmentType type;
        public int count;
        public EquipQualityType rarity;

        public RoleEquipData()
        { }

        public RoleEquipData(int id, int level, EquipmentType type,int count, EquipQualityType rarity)
        {
            this.id = id;
            this.level = level;
            this.type = type;
            this.count = count;
            this.rarity = rarity;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
        }

    }

}