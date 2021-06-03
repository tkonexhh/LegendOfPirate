using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class RoleModel : Model
    {
        public int id;
        public IntReactiveProperty level;
        public string name;
        public string resName;
        public ReactiveCollection<RoleEquipModel> equipList;
        public ReactiveCollection<RoleSkillModel> skillList;

        public RoleModel(RoleData roleData)
        {
            id = roleData.id;
            level = new IntReactiveProperty(roleData.level);
            name = roleData.name;

            equipList = new ReactiveCollection<RoleEquipModel>();
            for (int i = 0; i < roleData.equipList.Count; i++)
            {
                RoleEquipModel equipModel = new RoleEquipModel(roleData.equipList[i]);
                equipList.Add(equipModel);
            }

            skillList = new ReactiveCollection<RoleSkillModel>();
            for (int i = 0; i < roleData.skillList.Count; i++)
            {
                RoleSkillModel skillModel = new RoleSkillModel(roleData.skillList[i]);
                skillList.Add(skillModel);
            }
        }

        public bool AddSkill()
        {
            return false;
        }

        public void UpgradeSkill()
        {

        }

        public bool AddEquip()
        {
            return false;
        }

        public void UpgradeEquip()
        {

        }
    }

}