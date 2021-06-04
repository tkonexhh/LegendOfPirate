using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class RoleController : Controller
    {
        public int id;
        public int level;
        public string name;
        public string resName;

       
        public int curExp;
        public int starLevel;

        public int curHp;
        public float curCe;

        public Dictionary<EquipType, RoleEquip> roleEquipDic;
        public RoleModel roleModel;


        #region Override

        public override void OnInit()
        {
            base.OnInit();

        }

        public override void OnUpdate() { }
        public override void OnDestroyed() { }

        public override void OnCacheReset()
        {
            base.OnCacheReset();
            OnDestroyed();
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
            //ObjectPool<RoleController>.S.Recycle(this);
            RoleControllerFactory.S.RecycleController(this);
        }

        #endregion

        #region Public

        public void InitRoleEquipDic()
        {
            if(roleEquipDic == null)
                roleEquipDic = new Dictionary<EquipType, RoleEquip>();

            
        }

        //public RoleController EquipSkill(RoleSkill roleSkill)
        //{
        //    return this;
        //}


        public RoleEquip GetRoleEquipDic(EquipType type)
        {
            RoleEquip equip;
            if (!roleEquipDic.TryGetValue(type, out equip))
                return null;
            return equip;
        }

        #endregion

        #region Private
        private void RefreshRoleControllerData()
        {
            //TO DO..
        }

        private void SaveRoleControllerDataToModel()
        {
            //TODO..
        }
        #endregion
    }

}