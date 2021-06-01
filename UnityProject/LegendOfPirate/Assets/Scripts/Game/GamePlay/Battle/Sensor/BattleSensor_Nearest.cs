using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_Nearest : BattleSensor_Distance
    {

        public BattleSensor_Nearest(BattleRoleController controller) : base(controller)
        {
        }

        public override BattleRoleController PickTarget()
        {
            m_OppositeCamp = BattleHelper.GetOppositeCamp(m_Controller.camp);
            var controllers = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(m_OppositeCamp);
            float distance = float.MaxValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                var d = Vector3.Distance(m_Controller.transform.position, controllers[i].transform.position);
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