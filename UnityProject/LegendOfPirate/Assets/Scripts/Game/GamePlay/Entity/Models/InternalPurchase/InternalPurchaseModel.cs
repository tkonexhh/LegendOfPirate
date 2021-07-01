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
    public class InternalPurchaseModel : DbModel
    {
        private InternalPurchaseData m_InternalPurchaseData = null;
        protected override void LoadDataFromDb()
        {
            m_InternalPurchaseData = GameDataMgr.S.GetData<InternalPurchaseData>();

        }
    }


    #region Class



    #endregion
}