using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Qarth;

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

        public override void OnUpgrade(int delta)
        {
            base.OnUpgrade(delta);

            tableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

            for (int i = 0; i < slotModelList.Count; i++)
            {
                slotModelList[i].OnTrainingRoomLevelUp();
            }

            EventSystem.S.Send(EventID.OnTrainingRoomUpgradeRefresh);
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

        public ReactiveProperty<TrainingRoomRoleState> trainState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private TrainingRoomModel m_TrainingRoomMode;
        private TrainingData.TrainingDataItem m_DbItem;

        public TrainingSlotModel(TrainingRoomModel trainingRoomModel, TrainingData.TrainingDataItem dbItem)
        {
            m_TrainingRoomMode = trainingRoomModel;
            m_DbItem = dbItem;

            this.slotId = dbItem.slotId;
            this.heroId = dbItem.heroId;
            this.trainState = new ReactiveProperty<TrainingRoomRoleState>(dbItem.trainState);

            switch (trainState.Value)
            {
                case TrainingRoomRoleState.Free:
                    break;
                case TrainingRoomRoleState.Training:
                    SetTime(dbItem.trainingStartTime);
                    RefreshRemainTime();
                    break;
                case TrainingRoomRoleState.Locked:

                    break;
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
            trainState.Value = TrainingRoomRoleState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndTraining();
        }

        public void SetTrainingSlotModelFree()
        {
            EndTraining();
        }

        public void SetTrainingSlotModelSelectedNotStart(int id)
        {
            heroId = id;
            trainState.Value = TrainingRoomRoleState.SelectedNotStart;
            m_DbItem.SetTrainState(TrainingRoomRoleState.SelectedNotStart);
        }

        public void SetTrainingRoomRoleState(TrainingRoomRoleState trainingRoomRoleState)
        {
            trainState.Value = trainingRoomRoleState;
            m_DbItem.SetTrainState(trainingRoomRoleState);
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

        public void OnTrainingRoomLevelUp()
        {
            if (trainState.Value == TrainingRoomRoleState.Locked && m_TrainingRoomMode.tableConfig.capacity >= slotId)
            {
                trainState.Value = TrainingRoomRoleState.Free;

                m_DbItem.OnUnlocked();
            }
        }
    }

}