using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;

namespace GameWish.Game
{
    #region Other Data Class

    #endregion
    public partial class BlackMarketPanel : AbstractAnimPanel
    {
        #region SerializeField
        [SerializeField] private GameObject m_BlackMarketCommodity;
        #endregion

        #region Data
        private ReactiveCollection<MarketCommodityMoel> m_MarketCommodityModels = new ReactiveCollection<MarketCommodityMoel>();
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            AllocatePanelData(args);

            BindModelToUI();
            BindUIToModel();

            OnClickAddListener();

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

            ReleasePanelData();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            m_RefreshBtn.OnClickAsObservable().Subscribe(_ => RefreshMarketCommoditys()).AddTo(this);
        }

        private void RefreshMarketCommoditys()
        {
            m_PanelData.blackMarketModel.RefreshCommoditys();
        }

        private void MonitoringDataList(int count)
        {
            try
            {
                if (BlackMarketModel.BLACKMARKET_COMMODITY_NUMBER == count)
                {
                    if (m_MarketCommodityModels.Count != count)
                    {
                        Log.e("Error : Count is different");
                        return;
                    }
                    ReactiveCollection<CommodityDBData> commodityDBData = m_PanelData.blackMarketModel.BlackMarketCommoditys;
                    for (int i = 0; i < commodityDBData.Count; i++)
                    {
                        m_MarketCommodityModels[i].OnRefresh(commodityDBData[i]);
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
            InitCommoditysData();

            CreteCommodityIten();
        }

        private void CreteCommodityIten()
        {
            foreach (var item in m_MarketCommodityModels)
            {
                BlackMarketCommodity commodity = Instantiate(m_BlackMarketCommodity, m_BlackMarketCommodityTra.transform).GetComponent<BlackMarketCommodity>();
                commodity.OnInit(item);
                item.SetDataModel(commodity);
            }
        }

        private void InitCommoditysData()
        {
            foreach (var item in m_PanelData.blackMarketModel.BlackMarketCommoditys)
            {
                m_MarketCommodityModels.Add(new MarketCommodityMoel(item));
            }
        }
        #endregion
    }
}
