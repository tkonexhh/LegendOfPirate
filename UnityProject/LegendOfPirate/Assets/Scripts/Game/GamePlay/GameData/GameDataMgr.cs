using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using System.Linq;

namespace GameWish.Game
{
    public enum DataMode
    {
        Server,
        Local
    }

    /// <summary>
    /// GameData对外交互类
    /// </summary>
    public class GameDataMgr : TSingleton<GameDataMgr>
    {
        public static DataMode s_DataMode = DataMode.Local;

        private List<IDataHandler> m_DataHanlderList;

        private PlayerInfoDataHandler m_PlayerInfoDataHandler = null;
        private RoleGroupDataHandler m_RoleGroupDataHandler = null;
        private ShipDataHandler m_ShipDataHandler = null;
        private InventoryDataHandler m_InventoryDataHandler = null;
        private KitchenDataHandler m_KitchenDataHandler = null;
        private FishingDataHandler m_FishingDataHandler = null;
        private ForgeDataHandler m_ForgeDataHandler = null;
        private GardenDataHandler m_GardenDataHandler = null;
        private LibraryDataHandler m_LibraryDataHandler = null;
        private ProcessingDataHandler m_ProcessingDataHandler = null;
        private LaboratoryDataHandler m_LaboratoryDataHandler = null;
        private TrainingDataHandler m_TrainingDataHandler = null;
        private BattleDataHandler m_BattleDataHandler = null;
        private TaskDataHandler m_TaskDataHandler = null;

        private int m_LoadDoneCount = 0;
        private Action m_OnLoadDoneCallback = null;

        #region Public

        public void Init(Action callback)
        {
            m_OnLoadDoneCallback = callback;

            m_DataHanlderList = new List<IDataHandler>();

            //1.Create Handler
            m_PlayerInfoDataHandler = new PlayerInfoDataHandler();
            m_RoleGroupDataHandler = new RoleGroupDataHandler();
            m_ShipDataHandler = new ShipDataHandler();
            m_InventoryDataHandler = new InventoryDataHandler();
            m_KitchenDataHandler = new KitchenDataHandler();
            m_FishingDataHandler = new FishingDataHandler();
            m_ForgeDataHandler = new ForgeDataHandler();
            m_GardenDataHandler = new GardenDataHandler();
            m_LibraryDataHandler = new LibraryDataHandler();
            m_ProcessingDataHandler = new ProcessingDataHandler();
            m_LaboratoryDataHandler = new LaboratoryDataHandler();
            m_TrainingDataHandler = new TrainingDataHandler();
            m_BattleDataHandler = new BattleDataHandler();
            m_TaskDataHandler = new TaskDataHandler();

            //2.Add To List
            m_DataHanlderList.Add(m_PlayerInfoDataHandler);
            m_DataHanlderList.Add(m_RoleGroupDataHandler);
            m_DataHanlderList.Add(m_ShipDataHandler);
            m_DataHanlderList.Add(m_InventoryDataHandler);
            m_DataHanlderList.Add(m_KitchenDataHandler);
            m_DataHanlderList.Add(m_FishingDataHandler);
            m_DataHanlderList.Add(m_ForgeDataHandler);
            m_DataHanlderList.Add(m_GardenDataHandler);
            m_DataHanlderList.Add(m_LibraryDataHandler);
            m_DataHanlderList.Add(m_ProcessingDataHandler);
            m_DataHanlderList.Add(m_TrainingDataHandler);
            m_DataHanlderList.Add(m_BattleDataHandler);
            m_DataHanlderList.Add(m_TaskDataHandler);
            m_DataHanlderList.Add(m_LaboratoryDataHandler);

            //3.Set Callback
            m_PlayerInfoDataHandler.LoadData(OnLoadDone);
            m_RoleGroupDataHandler.LoadData(OnLoadDone);
            m_ShipDataHandler.LoadData(OnLoadDone);
            m_InventoryDataHandler.LoadData(OnLoadDone);
            m_KitchenDataHandler.LoadData(OnLoadDone);
            m_FishingDataHandler.LoadData(OnLoadDone);
            m_ForgeDataHandler.LoadData(OnLoadDone);
            m_GardenDataHandler.LoadData(OnLoadDone);
            m_LibraryDataHandler.LoadData(OnLoadDone);
            m_ProcessingDataHandler.LoadData(OnLoadDone);
            m_TrainingDataHandler.LoadData(OnLoadDone);
            m_BattleDataHandler.LoadData(OnLoadDone);
            m_TaskDataHandler.LoadData(OnLoadDone);
            m_LaboratoryDataHandler.LoadData(OnLoadDone);

            RegisterEvents();
        }

        public List<String> GetAllDataPaths()
        {
            List<String> pathList = new List<string>();

            pathList.Add(PlayerInfoDataHandler.GetDataFilePathByType(typeof(PlayerInfoData)));
            m_DataHanlderList.ForEach(i =>
            {
                string type = PlayerInfoDataHandler.GetDataFilePathByType(i.GetDataClass().GetType());
                pathList.Add(type);
            });

            return pathList;
        }

        public void SaveDataToLocal()
        {
            m_DataHanlderList.ForEach(i => i.SaveDataToLocal());
        }

        /// <summary>
        /// 当需要保存数据到服务器时，手动调用此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="successCallback"></param>
        /// <param name="failedCallback"></param>
        public void SaveDataToServer<T>(Action successCallback, Action failedCallback) where T : IDataClass
        {
            if (GameDataMgr.s_DataMode != DataMode.Server)
            {
                failedCallback?.Invoke();
                return;
            }

            IDataHandler dataHandler = m_DataHanlderList.FirstOrDefault(i => i.GetDataClass().GetType() == typeof(T));
            if (dataHandler != null)
            {
                dataHandler.SaveDataToServer(successCallback, failedCallback);
            }
            else
            {
                Log.e("Data Not Found: " + typeof(T).ToString());
            }
        }

        public T GetData<T>() where T : IDataClass
        {
            IDataHandler dataHandler = m_DataHanlderList.FirstOrDefault(i => i.GetDataClass().GetType() == typeof(T));
            if (dataHandler != null)
            {
                T t = (T)dataHandler.GetDataClass();
                return t;
            }
            else
            {
                Log.e("Data Not Found: " + typeof(T).ToString());
            }

            return null;
        }

        #endregion

        #region Private
        private void RegisterEvents()
        {

        }

        private void HandleEvent(int eventId, params object[] param)
        {

        }

        private void OnLoadDone()
        {
            m_LoadDoneCount++;

            if (m_LoadDoneCount >= m_DataHanlderList.Count)
            {
                Log.i("Data load done!");

                if (m_OnLoadDoneCallback != null)
                {
                    m_OnLoadDoneCallback.Invoke();
                }
            }
        }
        #endregion

    }
}