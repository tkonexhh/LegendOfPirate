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
        }
        #endregion

        #region Private
        private void PrepareResponsiveProperties()
        {
            singleDraw = new IntReactiveProperty(SINGLE_DRAW);
            continuousPumping = new IntReactiveProperty(CONTINUOUS_PUMPING);
            remainingTimes = new IntReactiveProperty(m_PubData.PubDBData.remainingTimes);


        }
        #endregion
    }
}