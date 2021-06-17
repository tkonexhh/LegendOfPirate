using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_Nearest : BattleSensor
    {
        public BattleSensor_Nearest(PickTargetType type) : base(type)
        {
        }


        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.Role.GetControllersByCamp(m_OppositeCamp);
            float distance = float.MaxValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                var d = Vector3.Distance(picker.transform.position, controllers[i].transform.position);
                if (d < distance)
                {
                    distance = d;
                    index = i;
                }
            }

            if (index != -1)
            {
                return controllers[index];
            }

            return null;
        }

    }

}