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

        public override void InitWithEmptyData()
        {
            for (int i = 1; i <= Define.TRAINING_ROOM_MAX_SLOT; i++)
            {
                TrainingSlotData item = new TrainingSlotData(i);
                trainingItemList.Add(item);
            }
        }

        public override void OnDataLoadFinish()
        {
            //for (int i = 0; i < trainingItemList.Count; i++)
            //{
            //    trainingItemList[i].SetTrainingData(this);
            //}
        }

        //public void StartTrainingHero(int slotId, int heroId, DateTime time)
        //{
        //    TrainingDataItem? item = GetTrainDataItem(slotId);
        //    if (item != null)
        //    {
        //        item.Value.OnStartTraining(heroId, time);
        //    }
        //}

        //public void EndTrainingHero(int slotId)
        //{
        //    TrainingDataItem? item = GetTrainDataItem(slotId);
        //    if (item != null)
        //    {
        //        item.Value.OnEndTraining();
        //    }
        //}

        private TrainingSlotData? GetTrainDataItem(int slotId)
        {
            TrainingSlotData? item = trainingItemList.FirstOrDefault(i => i.slotId == slotId);
            if (item == null)
            {
                Log.e("TrainingDataItem Not Found: " + slotId);
            }

            return item;
        }


        [Serializable]
        public struct TrainingSlotData
        {
            public int slotId;
            public int heroId;
            public DateTime trainingStartTime;
            public TrainingSlotState trainState;

            private TrainingData m_TrainingData;

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

                GameDataMgr.S.GetData<TrainingData>().SetDataDirty();
            }

            public void OnHeroSelected(int heroId)
            {
                this.heroId = heroId;
                trainState = TrainingSlotState.HeroSelected;

                GameDataMgr.S.GetData<TrainingData>().SetDataDirty();
            }

            public void OnHeroUnselected()
            {
                trainState = TrainingSlotState.Free;

                GameDataMgr.S.GetData<TrainingData>().SetDataDirty();
            }

            public void OnEndTraining()
            {
                this.heroId = -1;
                this.trainingStartTime = default(DateTime);
                trainState = TrainingSlotState.Free;

                GameDataMgr.S.GetData<TrainingData>().SetDataDirty();
            }

            public void OnUnlocked()
            {
                trainState = TrainingSlotState.Free;

                //m_TrainingData.SetDataDirty();
                GameDataMgr.S.GetData<TrainingData>().SetDataDirty();
            }
        }
    }
}