using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_MinHp : BattleSensor_Hp
    {

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            m_OppositeCamp = BattleHelper.GetOppositeCamp(picker.camp);
            var controllers = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(m_OppositeCamp);
            int minHp = int.MaxValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].Data.buffedData.IsDead.Value)
                    continue;
                var hp = controllers[i].Data.buffedData.Hp;
                if (hp < minHp)
                {
                    minHp = hp;
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