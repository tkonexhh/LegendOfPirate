using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Buff : IBuff
    {
        public int id;
        public float time;//持续时间
        public int nowAppendNum = 0;
        public BuffModelHandler_Attribute AttributeHandler { get; set; }

        public Buff(int id)
        {
            this.id = id;
        }

        #region  IBuff
        public void OnAddBuff(BattleRoleModel model)
        {
            if (AttributeHandler != null)
            {
                AttributeHandler.OnAddBuff(model);
            }
        }
        public void OnRemoveBuff(BattleRoleModel model)
        {
            if (AttributeHandler != null)
            {
                AttributeHandler.OnRemoveBuff(model);
            }
        }
        #endregion

    }

}