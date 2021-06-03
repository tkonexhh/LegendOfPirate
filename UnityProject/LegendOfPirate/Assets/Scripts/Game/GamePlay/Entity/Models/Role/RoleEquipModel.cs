using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
	public class RoleEquipModel : Model
	{
        public int equipId;
        public IntReactiveProperty equipLevel;
        public IntReactiveProperty upgradeCost;

        private RoleEquipData m_DbData;

        public RoleEquipModel(RoleEquipData equipData)
        {
            m_DbData = equipData;

            equipId = equipData.id;
            equipLevel = new IntReactiveProperty(equipData.level);

            //TODO: Add Other Propterties
        }
    }
	
}