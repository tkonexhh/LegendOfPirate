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
        public BoolReactiveProperty isLocked;
        public IntReactiveProperty spiritCount;

        public IntReactiveProperty curHp;
        public FloatReactiveProperty curAtk;
        public IntReactiveProperty curExp;
        public IntReactiveProperty starLevel;
        public ReactiveCollection<RoleEquipModel> equipList;
        public ReactiveCollection<RoleSkillModel> skillList;

        public ReactiveProperty<ShipRoleStateId> stateId;

        public TDRoleConfig tdRoleConfig;



        private RoleData roleData;


        public RoleModel(int roleId)
        {
            #region FormData
            this.roleData = GameDataMgr.S.GetData<RoleGroupData>().GetRoleItem(roleId);
            id = roleId;
            isLocked = new BoolReactiveProperty(roleData.isLocked);
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
            //攻击 = 基础攻击 * 攻击成长系数 ^ (等级 - 1) * 星级系数 ^ (星级 - 1)  TODO...
            //curAtk = new FloatReactiveProperty(tdRoleConfig.initAtk * Mathf.Pow());
            stateId = new ReactiveProperty<ShipRoleStateId>(ShipRoleStateId.Idle);

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
        public void AddSkill(int skillId)
        {
            RoleSkillData skilldata = roleData.AddRoleSkill(skillId);
            if (skilldata != null)
            {
                RoleSkillModel skillModel = new RoleSkillModel(skilldata);
                skillList.Add(skillModel);
            }
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

        public void AddSpiritCount(int value)
        {
            spiritCount.Value += value;
        }

        public void AddCurHp(int value)
        {
            curHp.Value += value;
        }

        public void AddCurExp(int value)
        {
            curExp.Value += value;
        }
        #endregion

        #region Private

        private void ModelSubscribe()
        {
            isLocked.Subscribe(unlock =>
           {
               roleData.SetRoleUnlocked();
           });

            spiritCount.Subscribe(count => 
            {
                roleData.SetRoleSpiritCount(spiritCount.Value);
            });

            level.Subscribe(lv =>
            {
                roleData.SetRoleLevel(level.Value);
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