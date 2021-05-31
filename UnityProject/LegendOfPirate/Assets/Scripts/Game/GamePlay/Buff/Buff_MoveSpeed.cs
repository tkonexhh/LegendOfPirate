using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Buff_MoveSpeed : Buff
    {
        public Buff_MoveSpeed(int id, ModifyType modifyType, int value) : base(id, modifyType, value)
        {

        }


        public override void OnAddBuff(BattleRoleModel model)
        {
            if (this.modifyType == ModifyType.Add)
            {
                model.MoveSpeed += Value;
            }
            else
            {
                model.MoveSpeed *= Value;
            }

        }


        public override void OnRemoveBuff(BattleRoleModel model)
        {
            if (this.modifyType == ModifyType.Add)
            {
                model.MoveSpeed -= Value;
            }
            else
            {
                model.MoveSpeed /= Value;
            }
        }
    }

}