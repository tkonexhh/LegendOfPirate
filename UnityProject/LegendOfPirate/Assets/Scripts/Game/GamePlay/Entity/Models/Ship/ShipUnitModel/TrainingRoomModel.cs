﻿using System.Collections;
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
                TrainingSlotModel slotModel = new TrainingSlotModel(this, m_DbData.trainingItemList[i]);
                slotModelList.Add(slotModel);
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

                for (int i = 0; i < slotModelList.Count; i++)
                {
                    slotModelList[i].RefreshRemainTime();
                }
            }
        }
    }
    public class TrainingSlotModel : Model
    {
        public int slotId;
        public int heroId = -1;
        public FloatReactiveProperty trainRemainTime = new FloatReactiveProperty(-1);
        public bool isTraining = false;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private TrainingRoomModel m_TrainingRoomMode;
        private TrainingData.TrainingDataItem m_DbItem;

        public TrainingSlotModel(TrainingRoomModel trainingRoomMode, TrainingData.TrainingDataItem dbItem)
        {
            m_TrainingRoomMode = trainingRoomMode;
            m_DbItem = dbItem;

            this.slotId = dbItem.slotId;
            this.heroId = dbItem.heroId;
            this.isTraining = dbItem.isTraining;

            if (isTraining)
            {
                SetTime(dbItem.trainingStartTime);

                RefreshRemainTime();
            }
        }

        public void StartTraining(int heroId, DateTime startTime)
        {
            this.heroId = heroId;

            SetTime(startTime);

            m_DbItem.OnStartTraining(heroId, startTime);
        }

        public void EndTraining()
        {
            heroId = -1;
            trainRemainTime.Value = -1f;
            isTraining = false;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndTraining();
        }

        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            int totalTime = m_TrainingRoomMode.tableConfig.trainingTime;
            m_EndTime = m_StartTime + TimeSpan.FromSeconds(totalTime);
        }

        public void RefreshRemainTime()
        {
            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            trainRemainTime.Value = (float)remainTime;
        }
    }
}