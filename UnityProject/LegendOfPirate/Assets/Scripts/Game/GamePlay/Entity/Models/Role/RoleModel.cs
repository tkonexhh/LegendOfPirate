using System.Collections;
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
        public BoolReactiveProperty isUnlcok;
        public IntReactiveProperty spiritCount;

        public IntReactiveProperty curHp;
        public FloatReactiveProperty curAtk;
        public IntReactiveProperty curExp;
        public IntReactiveProperty starLevel;
        public ReactiveCollection<RoleEquipModel> equipList;
        public ReactiveCollection<RoleSkillModel> skillList;

        public TDRoleConfig tdRoleConfig;

        private RoleData roleData;
        

        public RoleModel(RoleData data)
        {
            #region FormData
            this.roleData = data;
            id = roleData.id;
            isUnlcok = new BoolReactiveProperty(roleData.isUnlock);
            spiritCount = new IntReactiveProperty(roleData.spiritCount);
            level = new IntReactiveProperty(roleData.level);
           

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
            #endregion

            tdRoleConfig = TDRoleConfigTable.GetData(id);
            name = tdRoleConfig.roleName;
            //血量 = 基础血量 * 等级系数 * 星级系数 * 装备系数  //还有一个装备系数未加
            curHp = new IntReactiveProperty();
            //攻击 = 基础攻击 * 攻击成长系数^(等级-1) * 星级系数 ^(星级-1)  TODO...
            //curAtk = new FloatReactiveProperty(tdRoleConfig.initAtk * Mathf.Pow()); 


            ModelSubscribe();
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
        public int GetCurHp()
        {
            return curHp.Value;
        }

        public float GetCurAtk()
        {
            return curAtk.Value;
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

        public void SetCurExp(int value)
        {
            curExp.Value = value;
        }

        public void SetStaraLevel(int value)
        {
            starLevel.Value = value;
        }

        public void SetCurHp(int value)
        {
            curHp.Value += value;
        }

        public void SetCurAtk(float value)
        {
            curAtk.Value = value;
        }
        #endregion

        #region Private

        private void ModelSubscribe()
        {
            isUnlcok.Subscribe( unlock => 
            {
                roleData.SetUnlocked();
            });

            spiritCount.Subscribe(count => { });

            level.Subscribe(lv => 
            {
                roleData.AddLevel(level.Value);
                curHp.Value = tdRoleConfig.initHp * lv * starLevel.Value; 
            });

            curHp.Subscribe(hp => { });

            //curAtk.Subscribe(atk => { });

            curExp.Subscribe(exp => { });

            starLevel.Subscribe(starlv => 
            {
                curHp.Value = tdRoleConfig.initHp * level.Value * starlv;
            });
        }

        #endregion
    }

}