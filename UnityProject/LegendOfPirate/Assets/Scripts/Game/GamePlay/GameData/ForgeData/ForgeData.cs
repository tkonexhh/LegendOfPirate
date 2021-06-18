using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class ForgeData : IDataClass
    {
        public ForgeDataItem forgeDataItem = new ForgeDataItem();
        public override void InitWithEmptyData()
        {
            forgeDataItem = new ForgeDataItem(ForgeStage.Free);
        }

        public override void OnDataLoadFinish()
        {

        }
    }

    [Serializable]
    public struct ForgeDataItem 
    {
        public int weaponId;
        public DateTime forgingStartTime;
        public ForgeStage forgeState;

        public ForgeDataItem(ForgeStage state)
        {
            weaponId = -1;
            forgingStartTime = default(DateTime);
            forgeState = ForgeStage.Free;
        }

        public void OnStartForge(int Weaponid, DateTime time)
        {
            this.weaponId = Weaponid;
            this.forgingStartTime = time;
            forgeState = ForgeStage.Forging;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }

        public void OnForgeFinish()
        {
            this.forgingStartTime = default(DateTime);
            forgeState = ForgeStage.ForgeComplate;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }

        public int OnGetWeapon()
        {
            int ret = weaponId;
            this.weaponId = -1;
            forgeState = ForgeStage.Free;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();

            return ret;
        }

        public void OnWeaponSelect(int weaponid)
        {
            this.weaponId = weaponid;
            forgeState = ForgeStage.Select;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }

        public void OnWeaponUnSelect()
        {
            this.weaponId = -1;
            forgeState = ForgeStage.Free;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }
    }
}