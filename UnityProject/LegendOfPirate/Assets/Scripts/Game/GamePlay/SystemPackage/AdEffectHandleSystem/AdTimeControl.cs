using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class AdTimeControl:TSingleton<AdTimeControl>
	{
        public static Dictionary<int, int> m_AdTimeDic = new Dictionary<int, int>();
        public static Dictionary<int, int> m_AdTimeDiclimitMax = new Dictionary<int, int>(); //最大限制
        public static Dictionary<int, int> m_AdTimeDiclimitMin = new Dictionary<int, int>(); //最小限制
        public static Dictionary<int, int> m_AdTimeDicStart = new Dictionary<int, int>(); //动态时间

        public int ShowPopTime = 30;



        public override void OnSingletonInit()
        {
            base.OnSingletonInit();

            if(m_AdTimeDic == null)
            {
                m_AdTimeDic = new Dictionary<int,int>();
                m_AdTimeDiclimitMax = new Dictionary<int, int>();
                m_AdTimeDiclimitMin = new Dictionary<int, int>();
            }

            m_AdTimeDic.Add(1, 60);
            m_AdTimeDic.Add(2, 40);

            m_AdTimeDiclimitMax.Add(1, 120); //120
            m_AdTimeDiclimitMin.Add(1, 90); //90

            m_AdTimeDiclimitMax.Add(2, 60); //90
            m_AdTimeDiclimitMin.Add(2, 40); //60

            m_AdTimeDicStart.Add(1, 90); //90
            m_AdTimeDicStart.Add(2, 40); //60

        }


        private int _1PostionID = 1;

        private int _2PostionID = 1;

        public void StartPopAd()
        {
            StartPostion1Ad();
            StartPostion2Ad();
        }



        public void StartPostion1Ad()
        {
            AdType adType = AdType.PowerUp;
            switch (_1PostionID)
            {
                case 1:
                    adType = AdType.PowerUp;
                    break; 
                case 2:
                    adType = AdType.SummonPowerUp;
                    break;
            }
            PopAdMgr.S.AwakePopAd(adType, m_AdTimeDic[1]);
            m_AdTimeDic[1] = m_AdTimeDicStart[1];

            _1PostionID = ++_1PostionID > 2 ? 1 : _1PostionID;
        }

        public void StartPostion2Ad()
        {
            AdType adType = AdType.SummonGiant;
            switch (_2PostionID)
            {
                case 1:
                    adType = AdType.SummonGiant;
                    break;                
                case 2:
                    adType = AdType.SummonReinforcements;
                    break;
                case 3:
                    adType = AdType.Supply;
                    break;  
            }
            PopAdMgr.S.AwakePopAd(adType, m_AdTimeDic[2]);
            m_AdTimeDic[2] = m_AdTimeDicStart[2];

            _2PostionID = ++_2PostionID > 2 ? 1 : _2PostionID;
        }


        public void AddPositionTime(int Pos, int time)
        {
            if (Pos != 1 && Pos != 2) return;

            m_AdTimeDicStart[Pos] += time;

            if(m_AdTimeDicStart[Pos] >= m_AdTimeDiclimitMax[Pos])
            {
                m_AdTimeDicStart[Pos] = m_AdTimeDiclimitMax[Pos];
            }

            if(m_AdTimeDicStart[Pos] <= m_AdTimeDiclimitMin[Pos])
            {
                m_AdTimeDicStart[Pos] = m_AdTimeDiclimitMin[Pos];
            }
            m_AdTimeDic[Pos] = m_AdTimeDicStart[Pos];

            if (Pos == 1)
            {
                StartPostion1Ad();
            }

            else if(Pos == 2)
            {
                StartPostion2Ad();
            }

        }





    }
	
}