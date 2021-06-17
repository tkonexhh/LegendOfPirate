using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_MinHp : BattleSensor
    {
        public BattleSensor_MinHp(PickTargetType type) : base(type)
        {
        }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.Role.GetControllersByCamp(m_OppositeCamp);
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