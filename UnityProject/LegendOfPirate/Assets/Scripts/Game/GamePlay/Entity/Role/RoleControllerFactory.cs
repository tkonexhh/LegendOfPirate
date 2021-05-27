using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class RoleControllerFactory : ControllerFactory<RoleController>
    {
        protected override void BuildController(RoleController controller, params object[] param)
        {
            base.BuildController(controller);

            // controller.EquipSkill(RoleSkillMgr.S.GetSkill(RoleSkillType.None)).AddBuff(1);
        }
    }

}