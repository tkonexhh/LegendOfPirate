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
    public class InternalPurchaseModel : DbModel
    {
        #region Data Layer
        private InternalPurchaseData m_InternalPurchaseData = null;

        public InternalPurchaseData InternalPurchaseData { get { return m_InternalPurchaseData; } }
        #endregion

        #region Model
        protected override void LoadDataFromDb()
        {
            try
            {
                m_InternalPurchaseData = GameDataMgr.S.GetData<InternalPurchaseData>();

                m_MonthCardConfig = TDMonthCardConfigTable.GetMonthCardConfig();

                HandleVipRegionData();

                HandleDailySelectionData();

                UpdateVipDailyData();

                UpdateDailyData();
            }
            catch (Exception e)
            {
                Log.e("e : " + e);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (vipState.Value && receiveToday.Value)
            {
                //TODO �����Ϸһֱ�򿪵���ʱ����ˢ��
                vipRefreshCountdown.Value = GetVipRefreshCountdown();
            }
            dailyRefreshCountdown.Value = GetDailyRefreshCountdown();
        }
        #endregion

        #region Vip Variable
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
        /// <summary>
        /// Vip����ʱʱ��
        /// </summary>
        public StringReactiveProperty vipRefreshCountdown;

        private MonthCardConfig m_MonthCardConfig;
        public MonthCardConfig MonthCardConfig { get { return m_MonthCardConfig; } }
        #endregion

        #region Daily Selection
        /// <summary>
        /// daily����ʱʱ��
        /// </summary>
        public StringReactiveProperty dailyRefreshCountdown;
        /// <summary>
        /// �ճ���������
        /// </summary>
        private ReactiveCollection<DailyDBData> m_DailyModels = null;

        public ReactiveCollection<DailyDBData> DailyModels { get { return m_DailyModels; } }
        #endregion

        #region Const
        public const int COMMON_DAY_1 = 1;

        public const int VIP_MONTH_30 = 30;
        public const int VIP_PROFIT = 1888;

        public const int DAILY_NUMBER = 3;
        #endregion

        #region Public
        #region Vip

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
            if (timeSpan.Days < COMMON_DAY_1)
            {
                Log.e("��ȡ����ʯ = " + m_MonthCardConfig.firstDiamond);
                GameDataMgr.S.GetData<PlayerInfoData>().AddDiamond(m_MonthCardConfig.firstDiamond);
                m_InternalPurchaseData.SetFirstCollectionTimes();
            }
            else if (timeSpan.Days <= VIP_MONTH_30 && timeSpan.Days >= COMMON_DAY_1)
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
            if (DateTime.Now.Hour<6)
                m_InternalPurchaseData.SetLastCollectionTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day-1, 6, 0, 0));
            else
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

        #region Daily Selection
        /// <summary>
        /// �����ճ���Ʒ��״̬
        /// </summary>
        /// <param name="id"></param>
        /// <param name="purchaseState"></param>
        public void SetDailyPurchaseState(int id, PurchaseState purchaseState = PurchaseState.Purchased)
        {
            m_InternalPurchaseData.SetDailyPurchaseState(id, purchaseState);
        }

        /// <summary>
        /// ˢ�����е��ճ���Ʒ
        /// </summary>
        public void RefreshAllDailyData()
        {
            m_DailyModels.Clear();
            m_InternalPurchaseData.ClearAllDailyID();
            CreateDailyDBData(DailSelectionType.Adv);
            CreateDailyDBData(DailSelectionType.Daily);
            CreateDailyDBData(DailSelectionType.Daily);
        }

        #endregion
        #endregion

        #region Private
        #region Vip
        /// <summary>
        /// ����VIP��״̬
        /// </summary>
        private void UpdateVipDailyData()
        {
            TimeSpan vipDays = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (vipDays.Days > VIP_MONTH_30)
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
            if (lastDays.Days >= COMMON_DAY_1)
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
            vipRefreshCountdown = new StringReactiveProperty();

            dailyRefreshCountdown = new StringReactiveProperty();
        }

        /// <summary>
        /// ��ȡVipˢ��ʱ��
        /// </summary>
        /// <returns></returns>
        private string GetVipRefreshCountdown()
        {
            DateTime lastTime =m_InternalPurchaseData.GetConsumptionData().lastCollectionTime;
            DateTime date = lastTime.AddDays(1);
            DateTime dueDate = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0);
            TimeSpan remaining = dueDate - DateTime.Now;
            if (remaining.TotalSeconds<=0)
            {
                UpdateVipDailyData();
                return "0";
            }
            return CommonMethod.SplicingTime((int)remaining.TotalSeconds);
        }

        /// <summary>
        /// ���ÿ����ȡ����ʯ(������һ��)
        /// </summary>
        /// <returns></returns>
        private int GetdailyDiamondsNumberInFirst()
        {
            TimeSpan timeSpan = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().vipPurchaseTime;
            if (timeSpan.Days < InternalPurchaseModel.COMMON_DAY_1)
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

        #region Daily Selection
        /// <summary>
        /// �����ճ���������
        /// </summary>
        private void HandleDailySelectionData()
        {
            m_DailyModels = m_InternalPurchaseData.GetDailyDataModels();

            if (m_DailyModels.Count == 0)
            {
                CreateDailyDBData(DailSelectionType.Adv);
                CreateDailyDBData(DailSelectionType.Daily);
                CreateDailyDBData(DailSelectionType.Daily);
            }
        }

        /// <summary>
        /// ˢ���ճ�����ĵ���ʱ
        /// </summary>
        /// <returns></returns>
        private string GetDailyRefreshCountdown()
        {
            DateTime date = m_InternalPurchaseData.GetConsumptionData().dailyInitialTime.AddDays(1);
            DateTime dueDate = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0);
            TimeSpan remaining = dueDate - DateTime.Now;
            if (remaining.TotalSeconds<=0)
            {
                UpdateDailyData();
                return "0";
            }
            return CommonMethod.SplicingTime((int)remaining.TotalSeconds);
        }
        private void UpdateDailyData()
        {
            TimeSpan afterTime = DateTime.Now - m_InternalPurchaseData.GetConsumptionData().dailyInitialTime;
            if (afterTime.Days >= COMMON_DAY_1)
            {
                RefreshAllDailyData();
                m_InternalPurchaseData.SetDailyInitialTime();
            }
        }

        /// <summary>
        /// �����ճ����������
        /// </summary>
        /// <param name="daily"></param>
        private void CreateDailyDBData(DailSelectionType daily)
        {
            if (m_DailyModels.Count > DAILY_NUMBER)
            {
                Log.e("Error : Daily should not be greater than 3");
                return;
            }

            int id = GetRandomDailyModel();
            if (!m_DailyModels.Any(i => i.id == id))
            {
                DailyDBData newDaily = new DailyDBData(id, daily);
                m_DailyModels.Add(newDaily);
                m_InternalPurchaseData.AddDailyDBData(newDaily);
            }
            else
                CreateDailyDBData(daily);
        }

        /// <summary>
        /// �������ճ���Ʒ
        /// </summary>
        /// <returns></returns>
        private int GetRandomDailyModel()
        {
            int index = UnityEngine.Random.Range(1, TDDailySelectionConfigTable.dailySelectionProperties.Length+1);

            return index;
        }
        #endregion
        #endregion
    }
}