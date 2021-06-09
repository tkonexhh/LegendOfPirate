using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class CommonMethod 
	{

        #region UI
        /// <summary>
        /// ���ݱ��е�Key����ȡ��Ӧ������
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetStringForTableKey(string key)
        {
            return TDLanguageTable.Get(key);
        }
        /// <summary>
        /// �ո�
        /// </summary>
        /// <returns></returns>
        public static string TextIndent()
        {
            return "<color=#FFFFFF00>----</color>";
        }
        /// <summary>
        /// �ո�
        /// </summary>
        /// <returns></returns>
        public static string TextEmptyOne()
        {
            return "<color=#FFFFFF00>--</color>";
        }
        public static string GetStrForColor(string color, string cont, bool table = false)
        {
            if (!table)
            {
                return "<color=" + color + ">" + cont + "</color>";
            }
            else
            {
                return "<color=" + color + ">" + GetStringForTableKey(cont) + "</color>";

            }
        }
        #endregion
        #region Other Method
        /// <summary>
        /// ��ȡ��or��or����
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetTenThousandOrMillion(long number)
        {
            if (number.ToString().Length > 12)
            {
                long MainNumber = number / 1000000000000;
                //long remainder = number % 1000000000000;
                string remainder = number.ToString().Substring(number.ToString().Length - 12, 12);
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "����";
            }
            else if (number.ToString().Length > 8)
            {
                long MainNumber = number / 100000000;
                //long remainder = number % 100000000;
                string remainder = number.ToString().Substring(number.ToString().Length - 8, 8);
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "��";
            }
            else if (number.ToString().Length > 4)
            {
                long MainNumber = number / 10000;
                string remainder = number.ToString().Substring(number.ToString().Length - 4, 4);
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "��";
            }
            else
            {
                return number.ToString();
            }
        }
        private static long GetThousand(long number)
        {
            string numStr = number.ToString();
            return long.Parse(numStr.Substring(numStr.Length - 4, 1));
        }
        #endregion
    }

}