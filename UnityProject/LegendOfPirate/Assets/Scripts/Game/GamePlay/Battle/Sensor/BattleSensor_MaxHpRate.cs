using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_MaxHpRate : BattleSensor
    {
        public BattleSensor_MaxHpRate(PickTargetType type) : base(type)
        {
        }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.BattleRendererComponent.GetControllersByCamp(m_OppositeCamp);
            int maxHpRate = int.MinValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].Data.buffedData.IsDead.Value)
                    continue;
                var rate = controllers[i].Data.buffedData.Hp / controllers[i].Data.buffedData.MaxHp;
                if (rate > maxHpRate)
                {
                    maxHpRate = rate;
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