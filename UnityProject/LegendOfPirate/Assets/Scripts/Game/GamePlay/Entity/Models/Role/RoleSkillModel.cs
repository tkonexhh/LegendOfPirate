using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
	public class RoleSkillModel : Model
	{
        public int skillId;
        public IntReactiveProperty skillLevel;
        public IntReactiveProperty upgradeCost;

        private RoleSkillData m_DbData;

        public RoleSkillModel(RoleSkillData equipData)
        {
            m_DbData = equipData;

            skillId = equipData.id;
            skillLevel = new IntReactiveProperty(equipData.level);

            //TODO: Add Other Propterties
        }
    }
	
}