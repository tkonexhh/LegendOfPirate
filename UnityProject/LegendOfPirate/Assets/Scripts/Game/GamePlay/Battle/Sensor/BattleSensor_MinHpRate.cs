using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_MinHpRate : BattleSensor
    {
        public BattleSensor_MinHpRate(PickTargetType type) : base(type)
        {
        }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(m_OppositeCamp);
            int minHpRate = int.MaxValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].Data.buffedData.IsDead.Value)
                    continue;
                var rate = controllers[i].Data.buffedData.Hp / controllers[i].Data.buffedData.MaxHp;
                if (rate < minHpRate)
                {
                    minHpRate = rate;
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