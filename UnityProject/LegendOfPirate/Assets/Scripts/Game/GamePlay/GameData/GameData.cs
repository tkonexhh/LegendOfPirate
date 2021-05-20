using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GameData : IDataClass
    {
        public PlayerInfoData playerInfoData = null;
        //public ShopData shopData = null;
        //public MainTaskData mainTaskData = null;

        public GameData()
        {
            SetDirtyRecorder(GameDataHandler.s_DataDirtyRecorder);
        }

        public override void InitWithEmptyData()
        {
            playerInfoData = new PlayerInfoData();
            playerInfoData.SetDefaultValue();

            //shopData = new ShopData();
            //shopData.SetDefaultValue();

            //mainTaskData = new MainTaskData();
            //mainTaskData.SetDefaultValue();

        }

        public override void OnDataLoadFinish()
        {
            playerInfoData.SetDirtyRecorder(m_Recorder);
            //shopData.SetDirtyRecorder(m_Recorder);
            //mainTaskData.SetDirtyRecorder(m_Recorder);
        }       
    }
}
