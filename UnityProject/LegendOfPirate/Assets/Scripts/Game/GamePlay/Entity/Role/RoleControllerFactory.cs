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

            InitRoleControllerData(controller, (RoleModel)model);

            // controller.EquipSkill(RoleSkillMgr.S.GetSkill(RoleSkillType.None)).AddBuff(1);
        }

        private void InitRoleControllerData(RoleController controller,RoleModel model)
        {
            controller.id = model.id;
            controller.level = model.id;
            controller.name = model.name;
            controller.resName = model.resName;
            controller.curExp = model.GetCurExp();
            controller.starLevel = model.GetStarLevel();
            controller.roleModel = model;
        }
    }

}