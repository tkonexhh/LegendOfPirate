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


        public List<BuffModelHandler_Status> StatusHandler { get; set; }
        public BuffModelHandler_Attribute AttributeHandler { get; set; }

        public Buff(int id)
        {
            this.id = id;
        }

        #region  IBuff
        public void OnAddAppendNum(BattleRoleModel model)
        {
            if (AttributeHandler != null)
            {
                AttributeHandler.OnAppendBuff(nowAppendNum, model);
            }
        }

        public void OnAddBuff(BattleRoleModel model)
        {
            if (AttributeHandler != null)
            {
                AttributeHandler.OnAddBuff(model);
            }

            if (StatusHandler != null)
            {
                for (int i = 0; i < StatusHandler.Count; i++)
                {
                    StatusHandler[i].OnAddBuff(model);
                }
            }
        }

        public void OnRemoveBuff(BattleRoleModel model)
        {
            if (AttributeHandler != null)
            {
                AttributeHandler.OnRemoveBuff(model);
            }

            if (StatusHandler != null)
            {
                for (int i = 0; i < StatusHandler.Count; i++)
                {
                    StatusHandler[i].OnRemoveBuff(model);
                }
            }
        }
        #endregion

    }

}