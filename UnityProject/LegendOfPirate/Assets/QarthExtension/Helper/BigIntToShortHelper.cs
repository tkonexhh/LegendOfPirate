using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System.Text;

namespace GameWish.Game
{
    public class BigIntToShortHelper
    {
        private static BigInteger m_Billion = new BigInteger("1000000000");
        private static BigInteger m_Million = new BigInteger("1000000");
        private static BigInteger m_Thousand = new BigInteger("1000");

        public static string BigToShortString(BigInteger num)
        {
            if (num > m_Billion)
            {
                return GetString(num, m_Billion,"B");
            }
            else if (num>m_Million)
            {
                return GetString(num ,m_Million,"M");
            }
            else if(num>m_Thousand)
            {
                return GetString(num, m_Thousand, "K");
            }
            else
            {
                return num.ToString();
            }
        }

        private static string GetString(BigInteger num , BigInteger index ,string sympol)
        {
            return
                                string.Format(num / index + "." + (num % index) / (index / 10) + sympol);
        }

        private static string GetString1(BigInteger num, BigInteger index, string sympol)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}.{1}", num / index, (num % index) / (index / 10) + sympol);
            return sb.ToString();
        }

    }
}