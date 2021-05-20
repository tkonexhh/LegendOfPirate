using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class GameDataHandler : DataClassHandler<GameData>
    {
        public static DataDirtyRecorder s_DataDirtyRecorder = new DataDirtyRecorder();

        public static string s_path { get { return dataFilePath; } }

        public GameDataHandler()
        {
            Load();
            //EnableAutoSave();
        }

        public GameData GetGameData()
        {
            return m_Data;
        }

        public void Save()
        {
            Save(true);
        }

        public PlayerInfoData GetPlayerInfodata()
        {
            return m_Data.playerInfoData;
        }

        //public ShopData GetShopData()
        //{
        //    return m_Data.shopData;
        //}

        //public MainTaskData GetMainTaskData()
        //{
        //    return m_Data.mainTaskData;
        //}
    }
}