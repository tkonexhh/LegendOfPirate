using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class RoleControllerFactory : ControllerFactory<RoleController>
    {
        protected override void BuildController(RoleController controller, IModel model)
        {
            base.BuildController(controller, model);


            // controller.EquipSkill(RoleSkillMgr.S.GetSkill(RoleSkillType.None)).AddBuff(1);
        }

    }

}