﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    public class RoleModel : Model
    {
        public int id;
        public IntReactiveProperty level;
        public string name;
        public string resName;

        public IntReactiveProperty curExp;
        public IntReactiveProperty starLevel;
        public ReactiveCollection<RoleEquipModel> equipList;
        public ReactiveCollection<RoleSkillModel> skillList;

        public RoleModel(RoleData roleData)
        {
            id = roleData.id;
            level = new IntReactiveProperty(roleData.level);
            name = roleData.name;

            curExp = new IntReactiveProperty(roleData.curExp);
            starLevel = new IntReactiveProperty(roleData.starLevel);

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

        #region Public Get

        public RoleEquipModel GetEquipModel(int equipId)
        {
            RoleEquipModel equipModel = equipList.FirstOrDefault(i => i.equipId == equipId);

            if (equipModel == null)
            {
                Log.e("Equip Model Not Found: " + equipId);
            }

            return equipModel;
        }

        public RoleSkillModel GetSkillModel(int skillId)
        {
            RoleSkillModel skillModel = skillList.FirstOrDefault(i => i.skillId == skillId);

            if (skillModel == null)
            {
                Log.e("Skill Model Not Found: " + skillId);
            }

            return skillModel;
        }

        public int GetCurExp()
        {
            return curExp.Value;
        }

        public int GetStarLevel()
        {
            return starLevel.Value;
        }

        #endregion

        #region Public Set
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

        public void SetCurExp(int exp)
        {
            curExp.Value = exp;
        }

        public void SetStaraLevel(int level)
        {
            starLevel.Value = level;
        }
        #endregion
    }

}