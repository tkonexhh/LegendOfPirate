using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace GameWish.Game
{
    public class TrainingRoomModel : ShipUnitModel
    {
        public TrainingRoomUnitConfig tableConfig;
        public List<TrainingSlotModel> slotModelList = new List<TrainingSlotModel>();

        private TrainingData m_DbData;

        public TrainingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

            m_DbData = GameDataMgr.S.GetData<TrainingData>();
            for (int i = 0; i < m_DbData.trainingItemList.Count; i++)
            {
                TrainingSlotModel slotModel = new TrainingSlotModel(m_DbData.trainingItemList[i]);
                slotModelList.Add(slotModel);
            }
        }

        public override void OnUpdate()
        {

        }

        public class TrainingSlotModel : Model
        {
            public int slotId;
            public int heroId = -1;
            public FloatReactiveProperty trainRemainTime = null;
            public bool isTraining = false;

            public TrainingSlotModel(TrainingData.TrainingDataItem dbItem)
            {
                this.slotId = dbItem.slotId;
                this.heroId = dbItem.heroId;
                this.isTraining = dbItem.isTraining;
            }

            public void StartTraining(int heroId, DateTime startTime)
            {
                this.heroId = heroId;
            }
        }
    }

}