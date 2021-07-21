using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace GameWish.Game
{
	[ModelAutoRegister]
	public class PubModel : DbModel
	{
        #region Data Layer
        private PubData m_PubData = null;

        private RandomWeightHelper<PubConfig> m_RewardRandom = new RandomWeightHelper<PubConfig>();
        public PubData PubData { get { return m_PubData; } }
        #endregion

        #region Const
        /// <summary>
        /// 单抽
        /// </summary>
        public const int SINGLE_DRAW = 1;
        /// <summary>
        /// 十连抽
        /// </summary>
        public const int CONTINUOUS_PUMPING = 10;
        /// <summary>
        /// 每日上限
        /// </summary>
        public const int DAILY_LIMIT = 1000;
        #endregion

        #region ResponsiveProperties
        public IntReactiveProperty singleDraw;
        public IntReactiveProperty continuousPumping;
        public IntReactiveProperty remainingTimes;
        #endregion

        #region DbModel
        protected override void LoadDataFromDb()
        {
            m_PubData = GameDataMgr.S.GetData<PubData>();

            PrepareResponsiveProperties();

            HandleAllJackpot();

            UpDateRemainingTimes();
        }
        #endregion

        #region Public
        /// <summary>
        /// 单次抽奖
        /// </summary>
        public PubConfig SingleDraw()
        {
            if (remainingTimes.Value==0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.PUB_CONT_Ⅰ);
                return default(PubConfig);
            }

            ReduceLuckyDrawNumber(SINGLE_DRAW);
            return m_RewardRandom.GetRandomWeightValue();
        }

        /// <summary>
        /// 十连抽
        /// </summary>
        public List<PubConfig> ContinuousPumping()
        {
            if (remainingTimes.Value == 0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.PUB_CONT_Ⅰ);
                return null;
            }
            else if (remainingTimes.Value>0 && remainingTimes.Value < 10)
            {
                return GetLotteryItems(remainingTimes.Value);
            }
            else
            {
                return GetLotteryItems(CONTINUOUS_PUMPING);
            }
        }

        #endregion

        #region Private
        /// <summary>
        /// 更新刷新时间和刷新次数
        /// </summary>
        private void UpDateRemainingTimes()
        {
            TimeSpan timeSpan = DateTime.Now - m_PubData.PubDBData.lastRefreshTime;
            if (timeSpan.Days >= 1)
            {
                m_PubData.RefreshRemainingTimes();
                m_PubData.RefreshLastTime();
                remainingTimes.Value = m_PubData.PubDBData.remainingTimes;
            }
        }

        /// <summary>
        /// 获得抽奖物品
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<PubConfig> GetLotteryItems(int number)
        {
            List<PubConfig> pubConfigs = new List<PubConfig>();
            for (int i = 0; i < number; i++)
            {
                pubConfigs.Add(m_RewardRandom.GetRandomWeightDeleteValueRecoverable());
            }

            m_RewardRandom.Recovery();
            ReduceLuckyDrawNumber(CONTINUOUS_PUMPING);

            //TODO  获得物品 和 减少钻石
            //ModelMgr.S.GetModel<InventoryModel>().AddInventoryItemCount();
            //GameDataMgr.S.GetData<PlayerInfoData>().AddDiamond();
            return pubConfigs;
        }

        /// <summary>
        /// 减少抽奖次数
        /// </summary>
        /// <param name="number"></param>
        private void ReduceLuckyDrawNumber(int number)
        {
            remainingTimes.Value = Mathf.Max(Define.INT_NUMBER_ZERO, remainingTimes.Value - number);
            m_PubData.ReduceLuckyDrawNumber(number);
        }

        /// <summary>
        /// 处理随机奖池
        /// </summary>
        private void HandleAllJackpot()
        {
            foreach (var item in TDPubTable.PubConfigs)
            {
                m_RewardRandom.AddWeightItem(item, item.pubItemDetails.weight);
            }
        }

        /// <summary>
        /// 准备响应式属性
        /// </summary>
        private void PrepareResponsiveProperties()
        {
            singleDraw = new IntReactiveProperty(SINGLE_DRAW);
            continuousPumping = new IntReactiveProperty(CONTINUOUS_PUMPING);
            remainingTimes = new IntReactiveProperty(m_PubData.PubDBData.remainingTimes);
        }
        #endregion
    }
}