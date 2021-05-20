using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
namespace GameWish.Game
{
    
    public class AdsMgrExtend : TSingleton<AdsMgrExtend>
    {

  
        public override void OnSingletonInit()
        {
        }

         /// <summary>
         /// 判断广告是否准备好
         /// </summary>
        public bool AdsIsReadyByName()
        {
            bool isAdReady = CheckAdsIsReady();
            if(!isAdReady)
            {
                FloatMessage.S.ShowMsg(TDLanguageTable.Get("NoAdWarning"));
            }
            return isAdReady;
        }

        public bool CheckAdsIsReady()
        {
#if UNITY_EDITOR
            return true;
#else
            bool isAdReady = AdsMgr.S.GetAdInterface("Reward0").isAdReady;
            return isAdReady;
#endif
        }

        /// <summary>
        /// 判断广告是否准备好
        /// </summary>
        public bool AdsIsReadyByName(string str)
        {
            bool isAdReady = AdsMgr.S.GetAdInterface(str).isAdReady;
            return isAdReady;
        }

    }

}