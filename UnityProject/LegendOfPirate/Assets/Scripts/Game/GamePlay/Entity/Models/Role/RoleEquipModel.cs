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
        public Dictionary<EquipAttributeType,float> equipAttributeDic;
        private RoleEquipData m_DbData;
        public EquipmentUnitConfig equipConfig;
        public RoleEquipModel(RoleEquipData equipData)
        {
            m_DbData = equipData;

            equipId = equipData.id;
            equipType = equipData.type;
            equipConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(equipId);
            equipAttributeDic = new Dictionary<EquipAttributeType, float>();
            foreach (var item in equipConfig.equipAttributeValues) 
            {
                equipAttributeDic.Add(item.equipAttrType, item.percentage);
            }
            equipRarity = equipConfig.equipQualityType;
            upgradeCost = new IntReactiveProperty(equipConfig.coinCostNumber);

            equipLevel = new IntReactiveProperty(equipData.level);
            equipCount = new IntReactiveProperty(equipData.count);

            //TODO: Add Other Propterties
        }
        public void OnLevelUp(int deleta=1) 
        {
            m_DbData.Upgrade(deleta);
            equipId = m_DbData.id;
            equipConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(equipId);
            equipAttributeDic.Clear();
            foreach (var item in equipConfig.equipAttributeValues)
            {
                equipAttributeDic.Add(item.equipAttrType, item.percentage);
            }
            equipRarity = equipConfig.equipQualityType;
            upgradeCost.Value = equipConfig.coinCostNumber;

            equipLevel.Value = m_DbData.level;
            equipCount.Value = m_DbData.count;

        }

        public float GetAttributeValue(EquipAttributeType type) 
        {
            if (equipAttributeDic.ContainsKey(type)) 
            {
                return equipAttributeDic[type];
            }
            return 1;
        }

        
    }
	
}