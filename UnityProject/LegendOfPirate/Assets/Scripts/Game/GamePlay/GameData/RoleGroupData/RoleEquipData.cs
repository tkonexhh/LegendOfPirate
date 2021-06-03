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

        public RoleEquipData(int id, int level)
        {
            this.id = id;
            this.level = level;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
        }

    }

}