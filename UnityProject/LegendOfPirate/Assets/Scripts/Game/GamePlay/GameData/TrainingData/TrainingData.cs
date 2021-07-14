using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class TrainingData : IDataClass
    {
        public List<TrainingDBData> trainingItemList = new List<TrainingDBData>();

        public List<TrainingDBData> TrainingItemList { get { return trainingItemList; } }

        #region IDataClass
        public void SaveManually()
        {
            SetDataDirty();

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
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="slot"></param>
        public void AddTrainingSlotData(TrainingDBData slot)
        {
            if (!trainingItemList.Any(i => i.slotId == slot.slotId))
                trainingItemList.Add(slot);
            else
                Log.e("SortId is exit , SortId = " + slot.slotId);

            SaveManually();
        }

        /// <summary>
        /// 设置坑位的状态
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="traingSlotState"></param>
        public void SetTrainingSlotState(int slotID, TrainingSlotState traingSlotState)
        {
            TrainingDBData trainingDBData = trainingItemList.FirstOrDefault(i => i.slotId == slotID);
            if (trainingDBData != null)
            {
                if (traingSlotState == TrainingSlotState.Training)
                    trainingDBData.trainingStartTime = DateTime.Now;
                else if (traingSlotState == TrainingSlotState.Free)
                    trainingDBData.trainingStartTime = default(DateTime);
                trainingDBData.trainingState = traingSlotState;
            }
            else
                Log.e("SortID is exit , SortID = " + slotID);

            SaveManually();
        }

        /// <summary>
        /// 设置训练角色的ID
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="heroID"></param>
        public void SetTrainingRoleID(int slotID, int heroID)
        {
            TrainingDBData trainingDBData = trainingItemList.FirstOrDefault(i => i.slotId == slotID);
            if (trainingDBData != null)
            {
                trainingDBData.heroId = heroID;
            }
            else
                Log.e("SortID is exit , SortID = " + slotID);

            SaveManually();
        }
        #endregion
    }
}