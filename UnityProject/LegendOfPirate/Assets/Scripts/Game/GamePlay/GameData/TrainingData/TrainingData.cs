using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{   
    public class TrainingData : IDataClass
    {
        public List<TrainingSlotData> trainingItemList = new List<TrainingSlotData>();

        #region IDataClass
        public void SetDefaultValue()
        {
            SetDataDirty();

            //TODO 暂时使用
            GameDataMgr.S.SaveDataToLocal();
        }

        public override void InitWithEmptyData()
        {
        }

        public override void OnDataLoadFinish()
        {
         
        }
        #endregion
        #region Public
        public void AddTrainingSlotData(TrainingSlotData trainingSlotData)
        {
            if (!trainingItemList.Any(i => i.slotId == trainingSlotData.slotId))
                trainingItemList.Add(trainingSlotData);
            else
                Log.e("SortId is exit , SortId = " + trainingSlotData.slotId);

            SetDefaultValue();
        }
        #endregion

        #region Private
        private TrainingSlotData GetTrainDataItem(int slotId)
        {
            TrainingSlotData item = trainingItemList.FirstOrDefault(i => i.slotId == slotId);
            if (item == null)
            {
                Log.e("TrainingDataItem Not Found: " + slotId);
            }

            return item;
        }
        #endregion

        [Serializable]
        public class TrainingSlotData
        {
            public int slotId;
            public int heroId;
            public DateTime trainingStartTime;
            public TrainingSlotState trainState;

            private TrainingData m_TrainingData;

            public TrainingSlotData() { }

            public TrainingSlotData(int slot)
            {
                m_TrainingData = null;
                slotId = slot;
                heroId = -1;
                trainingStartTime = default(DateTime);
                trainState = TrainingSlotState.Locked;
            }

            public void OnStartTraining(int heroId, DateTime time)
            {
                this.heroId = heroId;
                this.trainingStartTime = time;
                trainState = TrainingSlotState.Training;

                SetDataDirty();
            }

            public void OnEndTraining()
            {
                this.heroId = -1;
                this.trainingStartTime = default(DateTime);
                trainState = TrainingSlotState.Free;

                SetDataDirty();
            }

            public void OnUnlocked()
            {
                trainState = TrainingSlotState.Free;

                //m_TrainingData.SetDataDirty();
                SetDataDirty();
            }

            private void SetDataDirty()
            {
                if (m_TrainingData == null)
                {
                    m_TrainingData = GameDataMgr.S.GetData<TrainingData>();
                }

                m_TrainingData.SetDefaultValue();
            }
        }
    }
}