using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public class MeasureUnitHelper 
    {
        private const string m_Thousand = "K";//1000
        private const string m_Millsion = "M";//1000K
        private const string m_Billion =  "B";//1000M
        private const int m_Scale = 1000;

        private static int m_BillCount;
        private static int m_MillCount;
        private static int m_KillCount;
        private static int m_NormalCount;

        private const string NORAML_COUNT_KEY = "normal_count_key";
        private const string THOUSAND_COUNT_KEY = "thousand_count_key";
        private const string MILLL_COUNT_KEY = "mill_count_key";
        private const string BILL_COUNT_KEY = "Bill_count_key";

        private static bool m_IsInit;

        public static string GetTotalCount()
        {
            
            Init();
            
            if (m_BillCount == 0)
            {
                return GetNoBillCount();
            }
            else
            {
                float inx = (float)m_MillCount / m_Scale;
                return ((float)m_BillCount + inx) + m_Billion;
            }
        }

        public static bool AddCount(int BCount,int MCount,int KCount, int normal)
        {
            
            Init();
            
            bool a = AddCount(normal);
            bool b = AddThousonudCount(KCount);
            bool c = AddMillCount(MCount);
            bool d = AddBillCount(BCount);

            if (a && b && c && d)
            {
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool  CompareToTotal(int BCount, int MCount, int KCount, int normal)
        {           
            Init();           
            if (BCount > m_BillCount)
            {
                return false;
            }
            else if (BCount == m_BillCount && MCount >m_MillCount) 
            {
                return false;
            }
            else if (MCount == m_MillCount && KCount > m_KillCount)
            {
                return false;
            }
            else if(KCount == m_KillCount && normal > m_NormalCount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void Init()
        {           
            m_BillCount = PlayerPrefs.GetInt(BILL_COUNT_KEY);
            m_MillCount = PlayerPrefs.GetInt(MILLL_COUNT_KEY);
            m_KillCount = PlayerPrefs.GetInt(THOUSAND_COUNT_KEY);
            m_NormalCount = PlayerPrefs.GetInt(NORAML_COUNT_KEY);
            
        }
       
        private static string GetNoBillCount()
        {
            if (m_MillCount == 0)
            {
                return GetNoMillCount();
            }
            else
            {
                float inx = (float)m_KillCount / m_Scale;
                return ((float)m_MillCount + inx) + m_Millsion;
            }

        }

        private static string GetNoMillCount()
        {
            if (m_KillCount == 0)
            {
                return GetNoKillCount();
            }
            else
            {
                float inx = (float)m_NormalCount / m_Scale;

                return ((float)m_KillCount + inx) + m_Thousand;
            }

            
        }

        private static string GetNoKillCount()
        {
            return m_NormalCount.ToString();
        }

        private static bool AddCount(int index)
        {            
            if (m_NormalCount + index < 0)
            {
                if (m_BillCount == 0 && m_MillCount == 0 && m_KillCount == 0)
                {
                    return false;
                }
                else
                {
                    m_NormalCount += m_Scale + index;
                    return AddThousonudCount(-1);
                }

            }
            else
            {
                m_NormalCount += index;
            }

            if (m_NormalCount / m_Scale > 0)
            {
                m_NormalCount = m_NormalCount % m_Scale;
                return AddThousonudCount(1);                
            }

            return true;

        }

        private static bool AddThousonudCount(int index)
        {

            if (m_KillCount + index < 0)
            {
                if (m_MillCount == 0 && m_BillCount == 0)
                {
                    return false;
                }
                else
                {
                    m_KillCount += m_Scale + index;
                    return AddMillCount(-1);
                    
                }
            }
            else
            {
                m_KillCount += index;
                           
            }
            if (m_KillCount / m_Scale > 0)
            {
                m_KillCount = m_KillCount % m_Scale;
                return AddMillCount(1);                
            }
            return true;
        }

        private static bool AddMillCount(int index)
        {
            if (m_MillCount + index < 0)
            {
                if (m_BillCount == 0) 
                {
                    return false;
                }
                else
                {
                    m_MillCount += m_Scale + index;
                    return AddMillCount(-1);
                    
                }
            }
            else
            {
                m_MillCount += index;                                   
            }

            if (m_MillCount / m_Scale > 0)
            {
                m_MillCount = m_MillCount % m_Scale;
                return AddBillCount(1);                
            }

            return true;
            
        }

        private static bool AddBillCount(int index)
        {
            m_BillCount += index;
            if (m_BillCount + index < 0)
            {
                return false;
            }
            return true;           
        }

        private static void Save()
        {
            PlayerPrefs.SetInt(BILL_COUNT_KEY, m_BillCount);
            PlayerPrefs.SetInt(MILLL_COUNT_KEY, m_MillCount);
            PlayerPrefs.SetInt(THOUSAND_COUNT_KEY, m_KillCount);
            PlayerPrefs.SetInt(NORAML_COUNT_KEY, m_NormalCount);
        }

    }
}