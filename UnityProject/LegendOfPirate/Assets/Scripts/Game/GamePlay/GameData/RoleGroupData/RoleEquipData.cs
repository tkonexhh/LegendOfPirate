using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public struct RoleEquipData
    {
        public int id;
        public int level;
        public EquipType type;
        public int count;
        public EquipRarity rarity;

        public RoleEquipData(int id, int level,EquipType type,int count,EquipRarity rarity)
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