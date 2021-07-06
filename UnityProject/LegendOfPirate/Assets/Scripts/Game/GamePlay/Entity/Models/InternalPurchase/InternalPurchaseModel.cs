using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class InternalPurchaseModel : DbModel
    {
        #region Data Layer
        private InternalPurchaseData m_InternalPurchaseData = null;

        private MonthCardConfig m_MonthCardConfig;

        public InternalPurchaseData InternalPurchaseData { get { return m_InternalPurchaseData; }}
        public MonthCardConfig MonthCardConfig { get { return m_MonthCardConfig; }}
        #endregion

        #region Model
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (vipState.Value && receiveToday.Value)
            {
                refreshCountdown.Value = GetRefreshCountdown();
            }
        }
        #endregion

        #region Vip
        /// <summary>
        /// Vip状态
        /// </summary>
        public BoolReactiveProperty vipState;
        /// <summary>
        /// 今天是否领取
        /// </summary>
        public BoolReactiveProperty receiveToday;
        /// <summary>
        /// 总共能领取的钻石
        /// </summary>
        public IntReactiveProperty totalDiamondsNumber;
        /// <summary>
        /// 今天购买可以领取的钻石
        /// </summary>
        public IntReactiveProperty todayDiamondsNumber;
        /// <summary>
        /// 每日领取的钻石(除了第一天之外)
        /// </summary>
        public IntReactiveProperty dailyDiamondsNumberNotFirst;
        /// <summary>
        /// 每日领取的钻石(包含第一天)
        /// </summary>
        public IntReactiveProperty dailyDiamondsNumberInFirst;
        /// <summary>
        /// 已经领取的钻石
        /// </summary>
        public IntReactiveProperty deceivedDiamondsNumber;
        /// <summary>
        /// 购买价格
        /// </summary>
        public FloatReactiveProperty purchasePrice;
        /// <summary>
        /// 购买利润
        /// </summary>
        public IntReactiveProperty purchaseProfit;
        /// <summary>
        /// 结束日期
        /// </summary>
        public ReactiveProperty<DateTime> vipDueDate;

        public StringReactiveProperty refreshCountdown;
        #endregion

        #region Const
        public const int VIP_MONTH_30 = 30;
        public const int VIP_MONTH_1 = 1;
        public const int VIP_PROFIT = 1888;
        #endregion

        protected override void LoadDataFromDb()
        {
            m_InternalPurchaseData = GameDataMgr.S.GetData<InternalPurchaseData>();

            m_MonthCardConfig = TDMonthCardConfigTable.GetMonthCardConfig();

            HandleVipRegionData();

            UpdateDailyData();
        }

        #region Public
        /// <summary>
        /// 购买Vip
        /// </summary>
        public void PurchaseVip()
        {
            SetVipState(true);

            SetAutomaticRenewal(true);

            SetVipPurchaseTime(DateTime.Now);

            vipDueDate.Value = GetVipDueDate();

            dailyDiamondsNumberInFirst.Value = GetdailyDiamondsNumberInFirst();

            SetReceiveToday(false);
        }

        /// <summary>
        /// 领取钻石
        /// </summary>
        public void CollectDiamonds()
        {
            TimeSpan timeSpan = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (timeSpan.Days < VIP_MONTH_1)
            {
                Log.e("领取了钻石 = " + m_MonthCardConfig.firstDiamond);
                GameDataMgr.S.GetData<PlayerInfoData>().AddDiamond(m_MonthCardConfig.firstDiamond);
                m_InternalPurchaseData.SetFirstCollectionTimes();
            }
            else if (timeSpan.Days <= VIP_MONTH_30 && timeSpan.Days >= VIP_MONTH_1)
            {
                Log.e("领取了钻石 = " + m_MonthCardConfig.dailyDiamond);
                GameDataMgr.S.GetData<PlayerInfoData>().AddDiamond(m_MonthCardConfig.dailyDiamond);
                m_InternalPurchaseData.SetDailyCollectionTimes();
            }
            else
            {
                m_InternalPurchaseData.OnReset();
                Log.e("erroe : More than 30 days");
                return;
            }
            deceivedDiamondsNumber.Value = GetDeceivedDiamonds();
            vipDueDate.Value = GetVipDueDate();
            SetReceiveToday(true);
            m_InternalPurchaseData.SetLastCollectionTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0));
            if (timeSpan.Days == VIP_MONTH_30)
            {
                //暂定领完自动续费
                m_InternalPurchaseData.AutomaticRenewalReset();
                PurchaseVip();
                return;
            }
        }

        #endregion

        #region Private
        private void UpdateDailyData()
        {
            TimeSpan vipDays = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (vipDays.Days> VIP_MONTH_30)
            {
                if (m_InternalPurchaseData.GetConsumptionData().automaticRenewal)
                {
                    //TODO 扣钱
                    Log.e("自动扣钱了");
                    m_InternalPurchaseData.AutomaticRenewalReset();
                    PurchaseVip();
                }
                else
                    SetVipState(false);
            }
            TimeSpan lastDays = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().lastCollectionTime;
            if (lastDays.Days>= VIP_MONTH_1)
            {
                SetReceiveToday(false);
            }
        }

        /// <summary>
        /// 设置Vip的状态
        /// </summary>
        /// <param name="state"></param>
        private void SetVipState(bool state)
        {
            vipState.Value = state;
            m_InternalPurchaseData.SetVipState(state);
            if (!state)
                m_InternalPurchaseData.OnReset();
        }

        /// <summary>
        /// 设置自动领取状态
        /// </summary>
        /// <param name="state"></param>
        private void SetAutomaticRenewal(bool state)
        {
            vipState.Value = state;
            m_InternalPurchaseData.SetAutomaticRenewal(state);
        }

        /// <summary>
        /// 设置今天领取状态 
        /// </summary>
        /// <param name="state"></param>
        private void SetReceiveToday(bool state)
        {
            receiveToday.Value = state;
            m_InternalPurchaseData.SetReceiveToday(state);
        }

        /// <summary>
        /// 设置Vip购买的时间 
        /// </summary>
        private void SetVipPurchaseTime(DateTime dateTime = default(DateTime))
        {
            if (dateTime != default(DateTime))
            {
                dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 6, 0, 0);
            }
            m_InternalPurchaseData.SetVipPurchaseTime(dateTime);
        }

        /// <summary>
        /// 处理Vip区域的数据
        /// </summary>
        private void HandleVipRegionData()
        {
            vipState = new BoolReactiveProperty(m_InternalPurchaseData.GetConsumptionData().vipState);
            receiveToday = new BoolReactiveProperty(m_InternalPurchaseData.GetConsumptionData().receiveToday);

            totalDiamondsNumber = GetTotalDiamondsNumber();
            todayDiamondsNumber = new IntReactiveProperty(m_MonthCardConfig.firstDiamond);
            dailyDiamondsNumberNotFirst = new IntReactiveProperty(m_MonthCardConfig.dailyDiamond);
            dailyDiamondsNumberInFirst = new IntReactiveProperty(GetdailyDiamondsNumberInFirst());
            deceivedDiamondsNumber = new IntReactiveProperty(GetDeceivedDiamonds());
            purchasePrice = new FloatReactiveProperty(GetPurchasePrice());
            purchaseProfit = new IntReactiveProperty(VIP_PROFIT);
            vipDueDate = new ReactiveProperty<DateTime>(GetVipDueDate());
            refreshCountdown = new StringReactiveProperty();
        }

        private string GetRefreshCountdown()
        {
            DateTime date = DateTime.Now.AddDays(1);
            DateTime dueDate = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0);
            TimeSpan remaining = dueDate - DateTime.Now;
            return CommonMethod.SplicingTime((int)remaining.TotalSeconds);
        }

        /// <summary>
        /// 获得每日领取的钻石(包含第一天)
        /// </summary>
        /// <returns></returns>
        private int GetdailyDiamondsNumberInFirst()
        {
            TimeSpan timeSpan = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (timeSpan.Days < InternalPurchaseModel.VIP_MONTH_1)
                return m_MonthCardConfig.firstDiamond;
            else
                return m_MonthCardConfig.dailyDiamond;
        }

        /// <summary>
        /// 获得已经领取了多少钻石
        /// </summary>
        /// <returns></returns>
        private int GetDeceivedDiamonds()
        {
            if (m_InternalPurchaseData.GetConsumptionData().vipState)
            {
                int deceivedDiamonds = 0;
                int firstTimes = m_InternalPurchaseData.GetConsumptionData().firstCollectionTimes;
                int dailyTimes = m_InternalPurchaseData.GetConsumptionData().dailyCollectionTimes;
                for (int i = 0; i < firstTimes; i++)
                    deceivedDiamonds += m_MonthCardConfig.firstDiamond;
                for (int i = 0; i < dailyTimes; i++)
                    deceivedDiamonds += m_MonthCardConfig.dailyDiamond;

                m_InternalPurchaseData.SetDeceivedDiamonds(deceivedDiamonds);
                return deceivedDiamonds;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获得Vip到期的时间 
        /// </summary>
        /// <returns></returns>
        private DateTime GetVipDueDate()
        {
            DateTime dueTime = m_InternalPurchaseData.GetConsumptionData().vipState
            ? m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime.AddDays(VIP_MONTH_30)
            : DateTime.Now.AddDays(VIP_MONTH_30);
            return dueTime;
        }

        /// <summary>
        /// 获得购买的钻石数量
        /// </summary>
        /// <returns></returns>
        private float GetPurchasePrice()
        {
            return m_InternalPurchaseData.GetConsumptionData().automaticRenewal ? m_MonthCardConfig.renewPrice : m_MonthCardConfig.price;
        }

        /// <summary>
        /// 获得总共能得到钻石数量
        /// </summary>
        /// <returns></returns>
        private IntReactiveProperty GetTotalDiamondsNumber()
        {
            int total = 0;
            total += m_MonthCardConfig.firstDiamond;
            for (int i = 0; i < VIP_MONTH_30 - 1; i++)
            {
                total += m_MonthCardConfig.dailyDiamond;
            }
            return new IntReactiveProperty(total);
        }

        #endregion
    }

    #region Class



    #endregion
}