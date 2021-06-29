using System.Collections.Generic;
using Qarth;
using QuickEngine.Extensions;
using System;

namespace GameWish.Game
{
    public class MainSeaLevelData : IDataClass
    {
        public int curlevelID;
        public List<MianSeaLevelItemData> listMianSeaLevelItemData;
        Dictionary<int, MianSeaLevelItemData> dicMianSeaLevelItemData;

        #region IDataClass

        public override void InitWithEmptyData()
        {
            listMianSeaLevelItemData = new List<MianSeaLevelItemData>();
            SetDataDirty();
        }

        public override void OnDataLoadFinish()
        {

        }

        #endregion

        #region Public Set



        #endregion

        #region Public Get

        public MianSeaLevelItemData GetTaskItemData(int keyId)
        {
            if (!GetDicItemData().ContainsKey(keyId))
            {
                MianSeaLevelItemData record = new MianSeaLevelItemData(keyId);
                GetDicItemData().TryAddKey(keyId, record);
                listMianSeaLevelItemData.Add(record);
                SetDataDirty();
            }
            return GetDicItemData()[keyId];
        }

        #endregion

        #region Private

        Dictionary<int, MianSeaLevelItemData> GetDicItemData()
        {
            if (dicMianSeaLevelItemData == null)
            {
                dicMianSeaLevelItemData = new Dictionary<int, MianSeaLevelItemData>();
                if (listMianSeaLevelItemData == null)
                {
                    listMianSeaLevelItemData = new List<MianSeaLevelItemData>();
                }

                for (int i = 0; i < listMianSeaLevelItemData.Count; i++)
                {
                    dicMianSeaLevelItemData.Add(listMianSeaLevelItemData[i].levelID, listMianSeaLevelItemData[i]);
                }
            }
            return dicMianSeaLevelItemData;
        }

        #endregion
    }

	
    [Serializable]
    public class MianSeaLevelItemData
    {
        public int levelID;
        public MainSeaLevelState levelState;
        public MainSeaIslandState isLandState;
        public DateTime productStartTime;


        private MainSeaLevelData m_MainSeaLevelData;
        public MianSeaLevelItemData() { }

        public MianSeaLevelItemData(int key)
        {
            levelID = key;
            levelState = MainSeaLevelState.Locked;
            isLandState = MainSeaIslandState.Producting;
            productStartTime = default(DateTime);
            NewDay();
            SetDataDirty();
        }

        public void SetLevelState(MainSeaLevelState state)
        {
            levelState = state;
            SetDataDirty();
        }

        public void SetIsLandState(MainSeaIslandState state)
        {
            isLandState = state;
            SetDataDirty();
        }

        public void OnStartProduct(int heroId, DateTime time)
        {
            productStartTime = time;
            isLandState = MainSeaIslandState.Producting;
            SetDataDirty();
        }

        public TimeSpan GetOfflineTime()
        {
            return DateTime.Now - productStartTime;
        }

        public void OnEndProduct()
        {
            productStartTime = default(DateTime);
            isLandState = MainSeaIslandState.Producted;
            SetDataDirty();
        }


        public void NewDay()
        {

        }


        private void SetDataDirty()
        {
            if (m_MainSeaLevelData == null)
            {
                m_MainSeaLevelData = GameDataMgr.S.GetData<MainSeaLevelData>();
            }
            m_MainSeaLevelData.SetDataDirty();
        }

    }

}