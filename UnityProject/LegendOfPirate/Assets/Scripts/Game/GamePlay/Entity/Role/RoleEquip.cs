using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class RoleEquip 
	{
		public int Id { get; set; }
		public EquipType Type { get; set; }

		public int Level { get; set; }
		public int Count { get; set; }


		public RoleEquip(int id,EquipType type, int level, int count)
        {
			this.Id = id;
			this.Type = type;
			this.Level = level;
			this.Count = count;

        }


		public void UpgradeLevel()
        {

        }

		public void AddCount()
        {

        }



	}
	
}