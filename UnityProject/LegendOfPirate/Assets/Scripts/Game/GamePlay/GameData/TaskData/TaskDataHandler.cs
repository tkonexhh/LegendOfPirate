using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public enum DailyTaskType
    {
        Min = 100,
        Ads,//广告
        Login,//登录
    }
    public enum AchievementTaskType
    {
        Ads,//广告
        Login,//登录
    }
    public class TaskDataHandler : DataHandlerBase<TaskData>, IDataHandler
    {
        private const string DATA_NAME = "TaskData";
        public TaskDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData(DATA_NAME, ParseJson, callback);
        }

        public override void SaveDataToServer(Action successCallback, Action failCallback)
        {
            base.SaveDataToServer(successCallback, failCallback);

            NetDataMgr.S.SaveNetData(DATA_NAME, m_Data, successCallback, failCallback);
        }
    }
}