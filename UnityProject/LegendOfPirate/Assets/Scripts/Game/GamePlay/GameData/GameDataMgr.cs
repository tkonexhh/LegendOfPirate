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

        private int m_LoadDoneCount = 0;
        private Action m_OnLoadDoneCallback = null;

        #region Public

        public void Init(Action callback)
        {
            m_OnLoadDoneCallback = callback;

            m_DataHanlderList = new List<IDataHandler>();

            m_PlayerInfoDataHandler = new PlayerInfoDataHandler();
            m_RoleGroupDataHandler = new RoleGroupDataHandler();
            m_ShipDataHandler = new ShipDataHandler();
            m_InventoryDataHandler = new InventoryDataHandler();
            m_KitchenDataHandler = new KitchenDataHandler();

            m_DataHanlderList.Add(m_PlayerInfoDataHandler);
            m_DataHanlderList.Add(m_RoleGroupDataHandler);
            m_DataHanlderList.Add(m_ShipDataHandler);
            m_DataHanlderList.Add(m_InventoryDataHandler);
            m_DataHanlderList.Add(m_KitchenDataHandler);

            m_PlayerInfoDataHandler.LoadData(OnLoadDone);
            m_RoleGroupDataHandler.LoadData(OnLoadDone);
            m_ShipDataHandler.LoadData(OnLoadDone);
            m_InventoryDataHandler.LoadData(OnLoadDone);
            m_KitchenDataHandler.LoadData(OnLoadDone);

            RegisterEvents();
        }

        public static List<String> GetAllDataPaths()
        {
            List<String> pathList = new List<string>();

            pathList.Add(PlayerInfoDataHandler.GetDataFilePathByType(typeof(PlayerInfoDataHandler)));

            return pathList;
        }

        public void Save()
        {
            m_PlayerInfoDataHandler.Save(null);
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