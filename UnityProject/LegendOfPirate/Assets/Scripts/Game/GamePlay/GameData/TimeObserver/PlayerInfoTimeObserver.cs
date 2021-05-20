using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class PlayerInfoTimeObserver : ITimeObserver
    {
        public int GetTickInterval()
        {
            return 1;
        }

        public int GetTickCount()
        {
            return -1;
        }

        public int GetTotalSeconds()
        {
            return -1;
        }

        public void OnFinished()
        {
 
        }

        public void OnPause()
        {
            GameDataMgr.S.GetPlayerInfoData().SetLastPlayTime(GameExtensions.GetTimeStamp());
            GameDataMgr.S.Save();
        }

        public void OnResume()
        {
            //GameDataMgr.S.GetPlayerInfoData().AddAdBoostTime(-OfflineTimeMgr.S.GetOfflineSecondsInInt());
        }

        public void OnStart()
        {
            //GameDataMgr.S.GetPlayerInfoData().AddAdBoostTime(-OfflineTimeMgr.S.GetOfflineSecondsInInt());
        }

        public bool ShouldRemoveWhenMapChanged()
        {
            return false;
        }

        public void OnTick(int count)
        {
            //GameDataMgr.S.GetPlayerInfoData().AddAdBoostTime(-1);

            SavePlayerTimeEveryMinute(count);
        }

        private static void SavePlayerTimeEveryMinute(int count)
        {
            if (count == 1 || count % 60 == 0)
            {
                GameDataMgr.S.GetPlayerInfoData().SetLastPlayTime(GameExtensions.GetTimeStamp());

                GameDataMgr.S.Save();
            }
        }
    }
}