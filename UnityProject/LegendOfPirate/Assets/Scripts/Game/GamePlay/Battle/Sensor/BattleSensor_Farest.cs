using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_Farest : BattleSensor
    {
        public BattleSensor_Farest(PickTargetType type) : base(type)
        {
        }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(m_OppositeCamp);
            float distance = float.MinValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                var d = Vector3.Distance(picker.transform.position, controllers[i].transform.position);
                if (d > distance)
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