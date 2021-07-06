using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    public enum RoleType
    {
        Front = 1,
        Mid = 2,
        Back = 3,
    }
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
        public ReactiveDictionary<EquipmentType, RoleEquipModel> equipDic;
        public ReactiveCollection<RoleSkillModel> skillList;
        public Dictionary<EquipmentType, int> equipLimitDic;
        public ReactiveProperty<ShipRoleStateId> stateId;
        public TDRoleConfig tdRoleConfig;
        public TDRoleStarUpConfig tdStarUpConfig;

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
            tdRoleConfig = TDRoleConfigTable.GetData(id);
            equipDic = new ReactiveDictionary<EquipmentType, RoleEquipModel>();
            equipLimitDic = tdRoleConfig.GetEquipLimit();
            for (int i = 0; i < roleData.equipDic.Count; i++)
            {
                if (roleData.equipDic.ContainsKey((EquipmentType)i))
                {
                    RoleEquipModel equipModel = new RoleEquipModel(roleData.equipDic[(EquipmentType)i]);
                    equipDic.Add((EquipmentType)i, equipModel);
                }
            }
            skillList = new ReactiveCollection<RoleSkillModel>();
            for (int i = 0; i < roleData.skillList.Count; i++)
            {
                RoleSkillModel skillModel = new RoleSkillModel(roleData.skillList[i]);
                skillList.Add(skillModel);
            }
            #endregion


            name = tdRoleConfig.roleName;
            //血量 = 基础血量 * 等级系数 * 星级系数 * 装备系数  //还有一个装备系数未加
            curHp = new IntReactiveProperty(
                (int)
                (tdRoleConfig.initHp *                                 //基础血量
                 GetGrowRateOfLevel()*                                 //等级系数
                 GetGrowRateOfStar() *                                 //星级系数
                 GetEquipRateByAttributeType(EquipAttributeType.HP))   //装备系数
                 ); 
            //攻击 = 基础攻击 * 攻击成长系数 ^ (等级 - 1) * 星级系数 ^ (星级 - 1)  TODO...
            curAtk = new FloatReactiveProperty(
                tdRoleConfig.initAtk *                                 //基础血量
                GetGrowRateOfLevel()*                                  //等级系数
                GetGrowRateOfStar()*                                   //星级系数
                GetEquipRateByAttributeType(EquipAttributeType.ATK)    //装备系数
                );
            stateId = new ReactiveProperty<ShipRoleStateId>(ShipRoleStateId.Idle);

            ModelSubscribe();
        }

        #region Public Get

        public RoleEquipModel GetEquipModel(int equipId)
        {
            RoleEquipModel equipModel = equipDic.FirstOrDefault(i => i.Value.equipId == equipId).Value;

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

        public RoleType GetRoleType()
        {
            return tdRoleConfig.GetRoleType();
        }

        public RoleEquipModel GetEquipModelByType(EquipmentType type)
        {
            RoleEquipModel output = null;
            equipDic.TryGetValue(type, out output);
            return output;
        }

        public int GetEquipModelIdByType(EquipmentType type) 
        {
            return equipLimitDic[type];
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

        public bool UpgradeSkill(int skillID, int delta = 1)
        {
            if (roleData.UpgradeRoleSkill(skillID, delta))
            {
                RoleSkillModel roleSkillModel = skillList.FirstOrDefault(i => i.skillId == skillID);
                roleSkillModel.UpgradeSkill(delta);
                roleSkillModel.SetLastSkillLevel(delta);
                return true;
            }
            else
            {
                Log.e("Upgrade error: = skillID" + skillID);
                return false;
            }
        }

        /// <summary>
        /// 添加新装备
        /// </summary>
        /// <param name="type"></param>
        public void AddEquip(EquipmentType type)
        {
            if (!equipDic.ContainsKey(type))
            {
                var id = equipLimitDic[type];
                var equipConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(id);
                if (roleData.AddRoleEquip(equipConfig.equipmentType, id))
                    equipDic.Add(equipConfig.equipmentType,
                        new RoleEquipModel(
                            new RoleEquipData(
                            equipConfig.equipmentID,
                            equipConfig.startLevel,
                            equipConfig.equipmentType,
                            1,
                            equipConfig.equipQualityType
                            )));
            }
        }
        /// <summary>
        /// 升级已有装备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="deleta"></param>
        public void UpgradeEquip(EquipmentType type, int deleta = 1)
        {
            if (equipDic.ContainsKey(type))
            {
                equipDic[type].OnLevelUp(deleta);
            }
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

            spiritCount.Where(count=>count<Define.ROLEGET_NEED_SPIRIT_COUNT).Subscribe(count =>
            {
                roleData.SetRoleSpiritCount(spiritCount.Value);
                
            });

            spiritCount.Where(count => count >= Define.ROLEGET_NEED_SPIRIT_COUNT).Subscribe(count=> 
            {
                spiritCount.Value -= Define.ROLEGET_NEED_SPIRIT_COUNT;
                UIMgr.S.OpenPanel(UIID.RoleGetPanel, this.id);
                ModelMgr.S.GetModel<RoleGroupModel>().SetRoleUnlockedModel(this.id);
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

        private float GetEquipRateByAttributeType(EquipAttributeType type)
        {
            float ret=  1;
            foreach (var equip in equipDic) 
            {
                if (equip.Value.equipAttributeDic.ContainsKey(type)) 
                {
                    ret *= equip.Value.equipAttributeDic[type];
                }
            }
            return ret;
        }

        private float GetGrowRateOfLevel() 
        {
            return Mathf.Pow(TDGlobalConfigTable.GetRoleAttributesGrowRateOfLevel(), (level.Value - 1));
        }

        private float GetGrowRateOfStar()
        {
            return Mathf.Pow(TDGlobalConfigTable.GetRoleAttributesGrowRateOfStar(), (starLevel.Value - 1));
        }
        #endregion
    }

}