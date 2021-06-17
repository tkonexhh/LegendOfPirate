using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleSensor_MaxHp : BattleSensor
    {
        public BattleSensor_MaxHp(PickTargetType type) : base(type)
        {
        }

        public override BattleRoleController PickTarget(BattleRoleController picker)
        {
            GetPickBattleCamp(picker);
            var controllers = BattleMgr.S.Role.GetControllersByCamp(m_OppositeCamp);
            int maxHp = int.MinValue;
            int index = -1;
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].Data.buffedData.IsDead.Value)
                    continue;
                var hp = controllers[i].Data.buffedData.Hp;
                if (hp > maxHp)
                {
                    maxHp = hp;
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