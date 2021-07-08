using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;
using System.Linq;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class BlackMarketModel : DbModel
    {
        #region Data Layer
        private BlackMarketData m_BlackMarketData = null;

        public BlackMarketData BlackMarketData { get { return m_BlackMarketData; } }
        #endregion

        #region DbModel
        protected override void LoadDataFromDb()
        {
            m_BlackMarketData = GameDataMgr.S.GetData<BlackMarketData>();

            RefreshNeedDiamonds = new IntReactiveProperty(GetRefreshNeedDiamonds());

            refreshCommodityCountDown = new StringReactiveProperty();

            HandleBlackMarketCommodity();

            UpdateDailyData();
        }
       
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (m_BlackMarketData.GetRefreshCount() < TDBlackMarketRefreshConfigTable.GetMaxRefreshID())
            {
                refreshCommodityCountDown.Value = GetRefreshCountDown();
            }
        }
        #endregion

        #region Variable
        public IntReactiveProperty RefreshNeedDiamonds;
        public StringReactiveProperty refreshCommodityCountDown;

        private ReactiveCollection<CommodityDBData> m_BlackMarketCommoditys;
        public ReactiveCollection<CommodityDBData> BlackMarketCommoditys { get { return m_BlackMarketCommoditys; } }
        #endregion

        #region Const
        public const int BLACKMARKET_COMMODITY_NUMBER = 9;

        public const int ONE_DAY = 1;
        #endregion

        #region Public
        /// <summary>
        /// ������Ʒ
        /// </summary>
        /// <param name="commodityID"></param>
        /// <param name="number"></param>
        public void PurchaseCommodity(int commodityID, int number)
        {
            m_BlackMarketData.ReduceCommodityNumber(commodityID, number);
        }

        /// <summary>
        /// ˢ����Ʒ
        /// </summary>
        /// <returns></returns>
        public bool RefreshCommoditys()
        {
            if (m_BlackMarketData.GetRefreshCount()< TDBlackMarketRefreshConfigTable.GetMaxRefreshID())
            {
                BlackMarketRefreshConfig blackMarketRefreshConfig = TDBlackMarketRefreshConfigTable.GetBlackMarketConfig(m_BlackMarketData.GetRefreshCount()+1);

                //TODO ������ʯ
                //ModelMgr.S.GetModel<PlayerInfoData>().AddDiamond(-blackMarketRefreshConfig.cost);

                m_BlackMarketData.SetRefreshCount();

                ResetCommoditys();

                BlackMarketRefreshConfig consume = TDBlackMarketRefreshConfigTable.GetBlackMarketConfig(m_BlackMarketData.GetRefreshCount() + 1);

                RefreshNeedDiamonds.Value = consume.cost;

                return true;
            }
            else
            {
                //TODO ��UI��ͼ
                RefreshNeedDiamonds.Value = 0;

                return false;
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// ���ˢ��ʱ��
        /// </summary>
        /// <returns></returns>
        private string GetRefreshCountDown()
        {
            DateTime dueDate = m_BlackMarketData.GetRefreshTime().AddDays(1);
            TimeSpan remaining = dueDate - DateTime.Now;
            if (remaining.TotalSeconds <= 0)
            {
                m_BlackMarketData.ResetRefreshCount();
                m_BlackMarketData.SetRefreshTime();
                ResetCommoditys();
                return "0";
            }
            return CommonMethod.SplicingTime((int)remaining.TotalSeconds);
        }

        /// <summary>
        /// ÿ�����ݸ���
        /// </summary>
        private void UpdateDailyData()
        {
            TimeSpan remaining = DateTime.Now - m_BlackMarketData.GetRefreshTime();
            if (remaining.Days > ONE_DAY)
            {
                m_BlackMarketData.ResetRefreshCount();
                m_BlackMarketData.SetRefreshTime();
                ResetCommoditys();
            }
        }

        /// <summary>
        /// ���ˢ�µ���ʯ��Ŀ
        /// </summary>
        /// <returns></returns>
        private int GetRefreshNeedDiamonds()
        {
            if (m_BlackMarketData.GetRefreshCount() < TDBlackMarketRefreshConfigTable.GetMaxRefreshID())
            {
                BlackMarketRefreshConfig consume = TDBlackMarketRefreshConfigTable.GetBlackMarketConfig(m_BlackMarketData.GetRefreshCount() + 1);

                return consume.cost;
            }
            //TODO ��ʱ
            return 0;
        }

        /// <summary>
        /// ������Ʒ
        /// </summary>
        private void ResetCommoditys()
        {
            m_BlackMarketData.ClearAllCommodity();

            for (int i = 0; i < BLACKMARKET_COMMODITY_NUMBER; i++)
                CreateCommodityDBData();
        }

        /// <summary>
        /// ���������Ʒ
        /// </summary>
        private void HandleBlackMarketCommodity()
        {
            m_BlackMarketCommoditys = m_BlackMarketData.GetCommodityDBDatas();

            if (m_BlackMarketCommoditys.Count == 0)
            {
                for (int i = 0; i < BLACKMARKET_COMMODITY_NUMBER; i++)
                {
                    CreateCommodityDBData();
                }
            }
        }

        /// <summary>
        /// ������Ʒ����
        /// </summary>
        private void CreateCommodityDBData()
        {
            try
            {
                if (m_BlackMarketCommoditys.Count > BLACKMARKET_COMMODITY_NUMBER)
                {
                    Log.e("Error : Black Market should not be greater than 9");
                    return;
                }

                BlackMarketConfig config = GetRandomMarketConfig();
                if (!m_BlackMarketCommoditys.Any(i => i.commodityID == config.marketConfigItem.id))
                {
                    m_BlackMarketData.AddCommodityDBData(config.id, config.marketConfigItem.id, config.marketConfigItem.count);
                }
                else
                    CreateCommodityDBData();
            }
            catch (Exception)
            {
                Log.e("Error : Table is error");
            }
        }

        /// <summary>
        /// �������ĺ�����Ʒ
        /// </summary>
        /// <returns></returns>
        private BlackMarketConfig GetRandomMarketConfig()
        {
            int index = UnityEngine.Random.Range(1, TDBlackMarketConfigTable.blackMarketProperties.Length+1);

            return TDBlackMarketConfigTable.GetBlackMarketConfig(index);
        }
        #endregion
    }
}