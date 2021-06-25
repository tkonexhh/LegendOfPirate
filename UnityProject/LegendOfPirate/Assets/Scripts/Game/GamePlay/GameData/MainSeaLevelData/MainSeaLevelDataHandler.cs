using Qarth;
using System;

namespace GameWish.Game
{
    //所有关卡状态
    public enum MainSeaLevelState
    {
        Locked,//未解锁
        CanChallenge,//可挑战
        Finished//挑战完成
    }
	//已攻占岛屿状态
    public enum MainSeaIslandState
    {
        Producting,//资源生产中
        Producted,//资源生产完成
    }

    public class MainSeaLevelDataHandler : DataHandlerBase<MainSeaLevelData>, IDataHandler
    {
        private const string DATA_NAME = "MainSeaLevelData";
        public MainSeaLevelDataHandler()
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