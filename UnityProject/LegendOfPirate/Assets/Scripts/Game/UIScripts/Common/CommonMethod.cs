using DG.Tweening;
using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace GameWish.Game
{
    public class CommonMethod
    {
        #region UI
        /// <summary>
        /// 根据表中的Key，获取相应的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetStringForTableKey(string key)
        {
            return TDLanguageTable.Get(key);
        }

        /// <summary>
        /// 空格
        /// </summary>
        /// <returns></returns>
        public static string TextIndent()
        {
            return "<color=#FFFFFF00>----</color>";
        }

        /// <summary>
        /// 空格
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

        /// <summary>
        /// TMP上翻效果
        /// </summary>
        /// <param name="currentScoreText"></param>
        /// <param name="curValue"></param>
        /// <param name="targetValue"></param>
        public static void TMPFlipUpEffect(TextMeshProUGUI currentScoreText, float curValue, float targetValue)
        {
            Sequence mScoreSequence = DOTween.Sequence();

            mScoreSequence.SetAutoKill(false);

            mScoreSequence.Append(DOTween.To(delegate (float value)
            {
                //向下取整
                var temp = Math.Floor(value);
                //向Text组件赋值
                currentScoreText.text = GetTenThousandOrMillion((long)temp);
            }, curValue, targetValue, 1.0f));
            //将更新后的值记录下来, 用于下一次滚动动画
            curValue = targetValue;
        }
        #endregion

        #region Other Method

        #region Public
        public static string SplicingTime(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(seconds));
            string str = "";

            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = "00:" + ts.Seconds.ToString("00");
            }

            return str;
        }

        public static DateTime GetDateTimeForString(string time)
        {
            try
            {
                DateTime dateTime;
                DateTime.TryParse(time,out dateTime);
                return dateTime;
            }
            catch (Exception e)
            {
                Log.e("e = " + e);
                return default(DateTime);
            }
        }

        /// <summary>
        /// 获取万or亿or万亿
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
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "万亿";
            }
            else if (number.ToString().Length > 8)
            {
                long MainNumber = number / 100000000;
                //long remainder = number % 100000000;
                string remainder = number.ToString().Substring(number.ToString().Length - 8, 8);
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "亿";
            }
            else if (number.ToString().Length > 4)
            {
                long MainNumber = number / 10000;
                string remainder = number.ToString().Substring(number.ToString().Length - 4, 4);
                return MainNumber.ToString() + "." + remainder.ToString()[0] + "万";
            }
            else
            {
                return number.ToString();
            }
        }

        /// <summary>
        /// 格式 October 23,2021(string)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetFormatDate(DateTime dateTime)
        {
            string str = string.Empty;
            switch (dateTime.Month)
            {
                case 1:
                    str += LanguageKeyDefine.MONTH_JANUARY;
                    break;  
                case 2:
                    str += LanguageKeyDefine.MONTH_FEBRUSRY;
                    break;
                case 3:
                    str += LanguageKeyDefine.MONTH_MARCH;
                    break;
                case 4:
                    str += LanguageKeyDefine.MONTH_APRIL;
                    break;
                case 5:
                    str += LanguageKeyDefine.MONTH_MAY;
                    break;
                case 6:
                    str += LanguageKeyDefine.MONTH_JUNE;
                    break;
                case 7:
                    str += LanguageKeyDefine.MONTH_JULY;
                    break;
                case 8:
                    str += LanguageKeyDefine.MONTH_AUGUST;
                    break;
                case 9:
                    str += LanguageKeyDefine.MONTH_SEPTEMBER;
                    break;
                case 10:
                    str += LanguageKeyDefine.MONTH_OCTOBER;
                    break;
                case 11:
                    str += LanguageKeyDefine.MONTH_NOVEMBER;
                    break;
                case 12:
                    str += LanguageKeyDefine.MONTH_DECEMBER;
                    break;
            }
            str += " ";
            str += dateTime.Day;
            str += ",";
            str += dateTime.Year;
            return str;
        }
        #endregion
        #region Private
        private static long GetThousand(long number)
        {
            string numStr = number.ToString();
            return long.Parse(numStr.Substring(numStr.Length - 4, 1));
        }
       
        #endregion
        #endregion
    }
}