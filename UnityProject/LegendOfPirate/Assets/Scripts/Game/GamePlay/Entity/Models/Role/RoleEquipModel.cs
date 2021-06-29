using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
	public class RoleEquipModel : Model
	{
        public int equipId;
        public EquipmentType equipType;
        public EquipQualityType equipRarity;
        public IntReactiveProperty equipLevel;
        public IntReactiveProperty upgradeCost;
        public IntReactiveProperty equipCount;

        private RoleEquipData m_DbData;

        public RoleEquipModel(RoleEquipData equipData)
        {
            m_DbData = equipData;

            equipId = equipData.id;
            equipType = equipData.type;
            equipLevel = new IntReactiveProperty(equipData.level);
            equipCount =new IntReactiveProperty(equipData.count);
            equipLevel = new IntReactiveProperty(equipData.level);

            //TODO: Add Other Propterties
        }
        public void OnLevelUp(int deleta=1) 
        {
            m_DbData.Upgrade(deleta);
        }
    }
	
}