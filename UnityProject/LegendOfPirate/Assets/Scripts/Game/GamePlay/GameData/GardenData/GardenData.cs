using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{   
    public class GardenData : IDataClass
    {
        public GardenDataItem gardenDataItem = new GardenDataItem();

        public override void InitWithEmptyData()
        {
            gardenDataItem = new GardenDataItem(GardenState.Free);
        }

        public override void OnDataLoadFinish()
        {

        }

    }

    [Serializable]
    public struct GardenDataItem 
    {
        public int seedId;
        public DateTime plantingStartTime;
        public GardenState gardenState;

        public GardenDataItem(GardenState state) 
        {
            seedId = -1;
            plantingStartTime = default(DateTime);
            gardenState = GardenState.Free;
        }

        public void OnStartPlant(int seedid, DateTime time) 
        {
            this.seedId = seedid;
            this.plantingStartTime = time;
            gardenState = GardenState.Plant;

            GameDataMgr.S.GetData<GardenData>().SetDataDirty();
        }

        public void OnPlantFinish() 
        {
            this.plantingStartTime = default(DateTime);
            gardenState = GardenState.WaitingHarvest;

            GameDataMgr.S.GetData<GardenData>().SetDataDirty();
        }

        public void OnPlantHarvest() 
        {
            this.seedId = -1;
            gardenState = GardenState.Free;

            GameDataMgr.S.GetData<GardenData>().SetDataDirty();
        }

        public void OnPlantSelect(int seedid) 
        {
            this.seedId = seedid;
            gardenState = GardenState.Select;

            GameDataMgr.S.GetData<GardenData>().SetDataDirty();
        }

        public void OnPlantUnSelect()
        {
            this.seedId = -1;
            gardenState = GardenState.Free;

            GameDataMgr.S.GetData<GardenData>().SetDataDirty();
        }
    }
}