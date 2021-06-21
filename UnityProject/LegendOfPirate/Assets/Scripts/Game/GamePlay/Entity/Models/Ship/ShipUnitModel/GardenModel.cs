using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace GameWish.Game
{
    public class GardenModel : ShipUnitModel
    {
        public GardenPlantModel gardenPlantModel;
        public GardenUnitConfig tableConfig;
        public ReactiveCollection<PlantSlotModel> plantSlotModels = new ReactiveCollection<PlantSlotModel>();

        private GardenData m_DbData;
        
        public GardenModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityGardenTable.GetConfig(level.Value);
            m_DbData = GameDataMgr.S.GetData<GardenData>();
            gardenPlantModel = new GardenPlantModel(this, m_DbData.gardenDataItem);
            gardenPlantModel.seedId = 0;
            gardenPlantModel.gardenState.Value = GardenState.Free;
            for (int i = 0; i < TDFacilityGardenTable.dataList.Count; i++)
            {
                PlantSlotModel item = default(PlantSlotModel);
                if (level.Value >= TDFacilityGardenTable.dataList[i].level)
                {
                    item = new PlantSlotModel(this, i, false);
                }
                else 
                {
                    item = new PlantSlotModel(this, i, true);
                }
                plantSlotModels.Add(item);
            }
        }

        private float m_RefreshTime = 0;
        private float m_RefreshInterval = 0.3f;
        
        public override void OnUpdate()
        {
            m_RefreshTime += Time.deltaTime;
            if (m_RefreshTime >= m_RefreshInterval)
            {
                m_RefreshTime = 0;
                gardenPlantModel.RefreshRemainTime();
            }
        }
        public override void OnLevelUpgrade(int delta)
        {
            base.OnLevelUpgrade(delta);

            tableConfig = TDFacilityGardenTable.GetConfig(level.Value);

            for (int i = 0; i < plantSlotModels.Count; i++)
            {
                plantSlotModels[i].OnGardenLevelUp();
            }
        }
        public PlantSlotModel GetPlantSlotModel(int slotindex)
        {
            if (plantSlotModels != null) 
            {
                return plantSlotModels[slotindex];
            }
            else 
            {
                return default(PlantSlotModel);
            }
        }
    }
    public class GardenPlantModel : Model
    {
        public int seedId;
        public DateTime StartTime;
        public FloatReactiveProperty plantRemainTime = new FloatReactiveProperty(-1);

        public ReactiveProperty<GardenState> gardenState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private GardenModel m_GardenModel;
        private GardenDataItem m_DbItem;

        public GardenPlantModel(GardenModel gardenModel, GardenDataItem gardenDataItem) 
        {
            m_GardenModel = gardenModel;
            m_DbItem = gardenDataItem;

            this.seedId = gardenDataItem.seedId;
            this.gardenState = new ReactiveProperty<GardenState>(gardenDataItem.gardenState);
            switch (gardenState.Value)
            {
                case GardenState.Free:
                    break;
                case GardenState.Plant:
                    SetTime(m_DbItem.plantingStartTime);
                    RefreshRemainTime();
                    break;
                case GardenState.Select:
                    break;
                case GardenState.WaitingHarvest:
                    break;
            }
        }
        
        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(m_GardenModel.tableConfig.plantingSped);
        }
        
        #region public
        
        public void StartPlant(DateTime startTime) 
        {
            gardenState.Value = GardenState.Plant;
            SetTime(startTime);
            m_DbItem.OnStartPlant(seedId, startTime);
        }
        
        public void OnPlantSelect(int id) 
        {
            seedId = id;
            gardenState.Value = GardenState.Select;
            m_DbItem.OnPlantSelect(id);
        }

        public void OnPlantUnSelect() 
        {
            seedId = -1;
            gardenState.Value = GardenState.Free;
            m_DbItem.OnPlantUnSelect();
        }
        
        public void EndPlant() 
        {
            plantRemainTime.Value = -1;
            gardenState.Value = GardenState.WaitingHarvest;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);
            m_DbItem.OnPlantFinish();
        }
        
        public void OnPlantHarvest() 
        {
            seedId = -1;
            gardenState.Value = GardenState.Free;
            m_DbItem.OnPlantHarvest();
        }
        
        public void RefreshRemainTime()
        {
            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            plantRemainTime.Value = (float)remainTime;
        }
        #endregion
    }
    public class PlantSlotModel : Model 
    {
        public BoolReactiveProperty slotIsUnlock;
        public string plantName;
        public int slotId;
        public int unlockLevel;
        private GardenModel m_GardenModel;
        public PlantSlotModel(GardenModel gardenModel,int slotid, bool unlockStage) 
        {
            this.slotId = slotid;
            this.unlockLevel = TDFacilityGardenTable.dataList[slotid].level;
            this.plantName = TDFacilityGardenTable.dataList[slotid].seedUnlock;
            m_GardenModel = gardenModel;
            slotIsUnlock = new BoolReactiveProperty(unlockStage);
        }
        public void OnGardenLevelUp() 
        {

            if (m_GardenModel.level.Value >= unlockLevel)
            {
                slotIsUnlock.Value = false;
            }
            else 
            {
                slotIsUnlock.Value = true;
            }
           
        }
     
    }

}