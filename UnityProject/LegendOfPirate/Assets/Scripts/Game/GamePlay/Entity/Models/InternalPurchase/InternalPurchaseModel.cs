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
        /// Vip״̬
        /// </summary>
        public BoolReactiveProperty vipState;
        /// <summary>
        /// �����Ƿ���ȡ
        /// </summary>
        public BoolReactiveProperty receiveToday;
        /// <summary>
        /// �ܹ�����ȡ����ʯ
        /// </summary>
        public IntReactiveProperty totalDiamondsNumber;
        /// <summary>
        /// ���칺�������ȡ����ʯ
        /// </summary>
        public IntReactiveProperty todayDiamondsNumber;
        /// <summary>
        /// ÿ����ȡ����ʯ(���˵�һ��֮��)
        /// </summary>
        public IntReactiveProperty dailyDiamondsNumberNotFirst;
        /// <summary>
        /// ÿ����ȡ����ʯ(������һ��)
        /// </summary>
        public IntReactiveProperty dailyDiamondsNumberInFirst;
        /// <summary>
        /// �Ѿ���ȡ����ʯ
        /// </summary>
        public IntReactiveProperty deceivedDiamondsNumber;
        /// <summary>
        /// ����۸�
        /// </summary>
        public FloatReactiveProperty purchasePrice;
        /// <summary>
        /// ��������
        /// </summary>
        public IntReactiveProperty purchaseProfit;
        /// <summary>
        /// ��������
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
        /// ����Vip
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
        /// ��ȡ��ʯ
        /// </summary>
        public void CollectDiamonds()
        {
            TimeSpan timeSpan = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (timeSpan.Days < VIP_MONTH_1)
            {
                Log.e("��ȡ����ʯ = " + m_MonthCardConfig.firstDiamond);
                GameDataMgr.S.GetData<PlayerInfoData>().AddDiamond(m_MonthCardConfig.firstDiamond);
                m_InternalPurchaseData.SetFirstCollectionTimes();
            }
            else if (timeSpan.Days <= VIP_MONTH_30 && timeSpan.Days >= VIP_MONTH_1)
            {
                Log.e("��ȡ����ʯ = " + m_MonthCardConfig.dailyDiamond);
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
                //�ݶ������Զ�����
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
                    //TODO ��Ǯ
                    Log.e("�Զ���Ǯ��");
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
        /// ����Vip��״̬
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
        /// �����Զ���ȡ״̬
        /// </summary>
        /// <param name="state"></param>
        private void SetAutomaticRenewal(bool state)
        {
            vipState.Value = state;
            m_InternalPurchaseData.SetAutomaticRenewal(state);
        }

        /// <summary>
        /// ���ý�����ȡ״̬ 
        /// </summary>
        /// <param name="state"></param>
        private void SetReceiveToday(bool state)
        {
            receiveToday.Value = state;
            m_InternalPurchaseData.SetReceiveToday(state);
        }

        /// <summary>
        /// ����Vip�����ʱ�� 
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
        /// ����Vip���������
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
        /// ���ÿ����ȡ����ʯ(������һ��)
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
        /// ����Ѿ���ȡ�˶�����ʯ
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
        /// ���Vip���ڵ�ʱ�� 
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
        /// ��ù������ʯ����
        /// </summary>
        /// <returns></returns>
        private float GetPurchasePrice()
        {
            return m_InternalPurchaseData.GetConsumptionData().automaticRenewal ? m_MonthCardConfig.renewPrice : m_MonthCardConfig.price;
        }

        /// <summary>
        /// ����ܹ��ܵõ���ʯ����
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