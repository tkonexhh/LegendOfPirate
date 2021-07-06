using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    [ModelAutoRegister]

    public class BlackMarketModel : DbModel
    {
        #region Data Layer
        private BlackMarketData m_BlackMarketData = null;

        public BlackMarketData BlackMarketData { get { return m_BlackMarketData; } }

        #endregion 
        protected override void LoadDataFromDb()
        {
            m_BlackMarketData = GameDataMgr.S.GetData<BlackMarketData>();
        }
    }
}