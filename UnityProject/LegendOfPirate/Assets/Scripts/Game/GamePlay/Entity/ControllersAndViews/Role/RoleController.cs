using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class RoleController : Controller
    {

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

            
        }

        //public RoleController EquipSkill(RoleSkill roleSkill)
        //{
        //    return this;
        //}

        #endregion

        #region Private

        #endregion
    }

}