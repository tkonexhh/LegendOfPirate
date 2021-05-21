using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class RoleController : Controller
	{
        protected RoleSkill m_EquipedSkill = null;

        #region Override

        public override void Init()
        {
            base.Init();
        }


        public override void OnCacheReset()
        {
            base.OnCacheReset();
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();

            ObjectPool<RoleController>.S.Recycle(this);
        }

        #endregion

        #region Public

        public RoleController EquipSkill(RoleSkill roleSkill)
        {
            m_EquipedSkill = roleSkill;

            return this;
        }

        public RoleController AddBuff(int test)
        {
            return this;
        }
        #endregion
    }

}