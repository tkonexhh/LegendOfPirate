using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class Buff : IBuff
    {
        public int id;
        public float time;//持续时间

        public int nowAppendNum = 0;
        public int maxAppendNum = 1;


        // public AttributeType AttributeType { get; private set; }
        public ModifyType modifyType { get; private set; }
        public int Value { get; private set; }

        public Buff(int id, ModifyType modifyType, int value)
        {
            this.id = id;
            this.modifyType = modifyType;
            this.Value = value;
        }

        #region  IBuff
        public abstract void OnAddBuff(BattleRoleModel model);
        public abstract void OnRemoveBuff(BattleRoleModel model);
        #endregion


        public void AddAppendNum()
        {
            nowAppendNum++;
            nowAppendNum = Mathf.Min(nowAppendNum, maxAppendNum);
        }
    }

}