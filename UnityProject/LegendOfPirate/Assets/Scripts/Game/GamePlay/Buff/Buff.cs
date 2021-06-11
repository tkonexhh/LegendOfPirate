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


        public BuffModelHandler_Status StatusHandler { get; set; }
        public List<BuffModelHandler_Attribute> AttributeHandler { get; set; }

        public Buff(int id)
        {
            this.id = id;
        }

        #region  IBuff
        public void OnAddAppendNum(BattleRoleRuntimeModel model)
        {
            if (AttributeHandler != null)
            {
                for (int i = 0; i < AttributeHandler.Count; i++)
                {
                    AttributeHandler[i].OnAppendBuff(nowAppendNum, model);
                }
            }
        }

        public void OnAddBuff(BattleRoleRuntimeModel model)
        {
            if (AttributeHandler != null)
            {
                for (int i = 0; i < AttributeHandler.Count; i++)
                {
                    AttributeHandler[i].OnAddBuff(model);
                }
            }

            if (StatusHandler != null)
            {
                StatusHandler.OnAddBuff(model);
            }
        }

        public void OnRemoveBuff(BattleRoleRuntimeModel model)
        {
            if (AttributeHandler != null)
            {
                for (int i = 0; i < AttributeHandler.Count; i++)
                {
                    AttributeHandler[i].OnRemoveBuff(model);
                }
            }

            if (StatusHandler != null)
            {
                StatusHandler.OnRemoveBuff(model);
            }
        }
        #endregion

    }

}