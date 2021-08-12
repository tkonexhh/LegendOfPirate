using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class ChargingInterfacePanel : AbstractAnimPanel
    {
        #region SerializeField
        [SerializeField] private GameObject m_DailySelectionItem;
        [SerializeField] private GameObject m_DiamondsItem;
        #endregion

        #region Data
        public List<DailySelectionModel> m_DailySelectionModels = new List<DailySelectionModel>();
        public List<DiamondsRegionModel> m_DiamondsRegionModels = new List<DiamondsRegionModel>();
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            BindModelToUI();

            BindUIToModel();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            InitData();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();

        }

        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }
        #endregion

        #region OnClickAsObservable
        private void HandlePurchaseBtn()
        {
            m_PanelData.internalPurchaseModel.PurchaseVip();
        }

        private void HandleObtainBtn()
        {
            m_PanelData.internalPurchaseModel.CollectDiamonds();
        }  
        private void HandleRefreshDailyModels(int count)
        {
            try
            {
                if (InternalPurchaseModel.DAILY_NUMBER == count)
                {
                    if (m_DailySelectionModels.Count != count)
                    {
                        Log.e("Error : Count is different");
                        return;
                    }
                    ReactiveCollection<DailyDBData> dailyDBDatas = m_PanelData.internalPurchaseModel.DailyModels;
                    for (int i = 0; i < dailyDBDatas.Count; i++)
                    {
                        m_DailySelectionModels[i].OnRefresh(dailyDBDatas[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Log.e("e: " + e);
            }
        }
        #endregion

        #region Private
        private void InitData()
        {
            InitDailyData();
            InitDiamondsData();

            CreateDailyItem();
            CreateDiamondsItem();
        }

        private void CreateDiamondsItem()
        {
            for (int i = 0; i < m_DiamondsRegionModels.Count; i++)
            {
                PurchaseDiamondsItem diamondsItem = Instantiate(m_DiamondsItem, m_PurchaseDiamondsRegion.transform).GetComponent<PurchaseDiamondsItem>();
                diamondsItem.OnInit(m_DiamondsRegionModels[i]);
                m_DiamondsRegionModels[i].SetModelData(diamondsItem);
            }
        }

        private void InitDiamondsData()
        {
            foreach (var item in TDPayShopConfigTable.payShopProperties)
            {
                m_DiamondsRegionModels.Add(new DiamondsRegionModel(item));
            }
        }

        private void InitDailyData()
        {
            ReactiveCollection<DailyDBData> dailyDBDatas = m_PanelData.internalPurchaseModel.DailyModels;
            for (int i = 0; i < m_PanelData.internalPurchaseModel.DailyModels.Count; i++)
            {
                m_DailySelectionModels.Add(new DailySelectionModel(dailyDBDatas[i]));
            }
        }

        private void CreateDailyItem()
        {
            for (int i = 0; i < m_DailySelectionModels.Count; i++)
            {
                DailySelectionItem dailyItem = Instantiate(m_DailySelectionItem, m_SelectionItemReion.transform).GetComponent<DailySelectionItem>();
                dailyItem.OnInit(m_DailySelectionModels[i]);
                m_DailySelectionModels[i].SetModelData(dailyItem);
            }
        }

        private string MachiningPurchaseProfit(int profit)
        {
            return string.Format(LanguageKeyDefine.INTERNALPURCHASE_PROFITTITLE, profit);
        }

        private string MachiningPurchasePrice(float price)
        {
            return string.Format(LanguageKeyDefine.INTERNALPURCHASE_PRICE, price);
        }

        private string MachiningVipDueDate(DateTime vueDate)
        {
            return string.Format(LanguageKeyDefine.INTERNALPURCHASE_DUEDATE, CommonMethod.GetFormatDate(vueDate));
        } 

        private string MachiningDailyRefresh(string val)
        {
            return string.Format(LanguageKeyDefine.INTERNALPURCHASE_DAILY_COUNTDOWN, val);
        }
        #endregion
    }
}
