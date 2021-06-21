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
            gardenDataItem = new GardenDataItem();
        }

        public override void OnDataLoadFinish()
        {

        }

    }

    [Serializable]
    public class GardenDataItem 
    {
        public int seedId;
        public DateTime plantingStartTime;
        public GardenState gardenState;

        private GardenData m_GardenData;

        public GardenDataItem() 
        {
            m_GardenData = null;
            seedId = -1;
            plantingStartTime = default(DateTime);
            gardenState = GardenState.Free;
        }

        public void OnStartPlant(int seedid, DateTime time) 
        {
            this.seedId = seedid;
            this.plantingStartTime = time;
            gardenState = GardenState.Plant;

            SetDataDirty();
        }

        public void OnPlantFinish() 
        {
            this.plantingStartTime = default(DateTime);
            gardenState = GardenState.WaitingHarvest;

            SetDataDirty();
        }

        public void OnPlantHarvest() 
        {
            this.seedId = -1;
            gardenState = GardenState.Free;

            SetDataDirty();
        }

        public void OnPlantSelect(int seedid) 
        {
            this.seedId = seedid;
            gardenState = GardenState.Select;

            SetDataDirty();
        }

        public void OnPlantUnSelect()
        {
            this.seedId = -1;
            gardenState = GardenState.Free;

            SetDataDirty();
        }

        private void SetDataDirty()
        {
            if (m_GardenData == null)
            {
                m_GardenData = GameDataMgr.S.GetData<GardenData>();
            }
            m_GardenData.SetDataDirty();
        }
    }
}