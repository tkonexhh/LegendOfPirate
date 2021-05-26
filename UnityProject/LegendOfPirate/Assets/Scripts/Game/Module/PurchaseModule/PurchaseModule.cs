using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using System.Reflection;

namespace GameWish.Game
{
    public class PurchaseModule : TSingleton<PurchaseModule>
    {
        private Type m_PurchaseServiceType;

        public Type purchaseServiceType
        {
            get
            {
                if (m_PurchaseServiceType == null)
                {
                    m_PurchaseServiceType = Type.GetType("GameWish.Game.PurchaseModule");
                }

                return m_PurchaseServiceType;
            }
        }

        public void Init()
        {
            EventSystem.S.Register(SDKEventID.OnPurchaseSuccess, OnPurchaseSuccess);
            PurchaseMgr.S.Init();
            PurchaseMgr.S.InitPurchaseInfo();
        }

        protected void OnPurchaseSuccess(int key, params object[] args)
        {
            TDPurchase data = args[0] as TDPurchase;

            Log.i("Iap " + data.id + " success");
            if (data != null)
            {
                if (data.isConsume)
                {
                    //TDPropItemConfig config = TDPropItemConfigTable.GetData(data.id);
                    //UIMgr.S.OpenTopPanel(UIID.EffectActivePanel,null,new ConfirmPanelData("",config.GetAssetName(),TDLanguageTable.Get("Feedback_ShopPurchase_Title"),"",TDLanguageTable.Get("Discription_"+config.itemID),delegate
                    //{
                    //    AddDiamondCount(data.itemNum);
                    //},null));
                }               
                else
                {
                    //TDPackageItemConfigTable.GetData(data.id).OnPurchaseSuccess();
                }
            }            
        }

        private void AddDiamondCount(int count)
        {

        }
    }
}
