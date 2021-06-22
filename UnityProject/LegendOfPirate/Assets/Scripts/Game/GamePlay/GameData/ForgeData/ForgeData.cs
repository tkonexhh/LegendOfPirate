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
    public class ForgeDataItem 
    {
        public int equipmentId;
        public DateTime forgingStartTime;
        public ForgeStage forgeState;

        public ForgeDataItem() { }

        public ForgeDataItem(ForgeStage state)
        {
            equipmentId = TDEquipmentSynthesisConfigTable.dataList[0].id;
            forgingStartTime = default(DateTime);
            forgeState = ForgeStage.Free;
        }

        public void OnStartForge(int Weaponid, DateTime time)
        {
            this.equipmentId = Weaponid;
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
            int ret = equipmentId;
            equipmentId = TDEquipmentSynthesisConfigTable.dataList[0].id;
            forgeState = ForgeStage.Free;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();

            return ret;
        }

        public void OnWeaponSelect(int weaponid)
        {
            this.equipmentId = weaponid;
            forgeState = ForgeStage.Select;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }

        public void OnWeaponUnSelect()
        {
            equipmentId = TDEquipmentSynthesisConfigTable.dataList[0].id;
            forgeState = ForgeStage.Free;

            GameDataMgr.S.GetData<ForgeData>().SetDataDirty();
        }
    }
}