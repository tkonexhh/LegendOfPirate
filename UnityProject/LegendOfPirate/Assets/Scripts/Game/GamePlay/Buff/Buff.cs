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

        public BuffModelHandler_Status StatusHandler { get; set; }//状态改变
        public List<BuffModelHandler_Attribute> AttributeHandler { get; set; }//属性改变
        public BuffTrigger BuffTrigger { get; set; }
        public BattleRoleController Owner { get; set; }//拥有者


        #region IBuff
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

        public void OnAddBuff()
        {
            BuffTrigger.Start(this);
        }

        public void OnRemoveBuff()
        {
            BuffTrigger.Stop(this);
            if (AttributeHandler != null)
            {
                for (int i = 0; i < AttributeHandler.Count; i++)
                {
                    AttributeHandler[i].OnRemoveBuff(Owner.Data.buffedData);
                }
            }

            if (StatusHandler != null)
            {
                StatusHandler.OnRemoveBuff(Owner.Data.buffedData);
            }
        }
        #endregion

        public void ExcuteBuff()
        {
            Debug.LogError("ExcuteBuff");
            if (AttributeHandler != null)
            {
                for (int i = 0; i < AttributeHandler.Count; i++)
                {
                    AttributeHandler[i].OnAddBuff(Owner.Data.buffedData);
                }
            }

            if (StatusHandler != null)
            {
                StatusHandler.OnAddBuff(Owner.Data.buffedData);
            }
        }

    }

}