using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using System.Globalization;

namespace GameWish.Game
{
    public static class StringHelper
    {
        //private static BigInteger THOUSAND = 1000;
        //private static BigInteger MILLION = 1000000;
        //private static BigInteger BILLION = 1000000000;
        //private static BigInteger TRILLION = 1000000000000;
        //private static BigInteger Q_BIG = 1000000000000000;

        //private static BigInteger Y_BIG = new BigInteger("1000000000000000000");
        //private static BigInteger U_BIG = new BigInteger("1000000000000000000000");
        //private static BigInteger P_BIG = new BigInteger("1000000000000000000000000");
        //private static BigInteger E_BIG = new BigInteger("1000000000000000000000000000");
        //private static BigInteger R_BIG = new BigInteger("1000000000000000000000000000000");
        //private static BigInteger N_BIG = new BigInteger("1000000000000000000000000000000000");
        //private static BigInteger G_BIG = new BigInteger("1000000000000000000000000000000000000");
        //private static BigInteger H_BIG = new BigInteger("1000000000000000000000000000000000000000");
        //private static BigInteger L_BIG = new BigInteger("1000000000000000000000000000000000000000000");
        //private static BigInteger S_BIG = new BigInteger("1000000000000000000000000000000000000000000000");
        //private static BigInteger V_BIG = new BigInteger("1000000000000000000000000000000000000000000000000");
        //private static BigInteger X_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000");
        //private static BigInteger Z_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000");
        //private static BigInteger D_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger F_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger W_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger O_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger C_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger I_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger J_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000000000000000");
        //private static BigInteger A_BIG = new BigInteger("1000000000000000000000000000000000000000000000000000000000000000000000000000000");


        //public static string FormatBigMoney(BigInteger num)
        //{
        //    if (num >= A_BIG)
        //    {
        //        return GetString(num, A_BIG, "A");
        //    }
        //    else if (num >= J_BIG)
        //    {
        //        return GetString(num, J_BIG, "J");
        //    }
        //    else if (num >= I_BIG)
        //    {
        //        return GetString(num, I_BIG, "I");
        //    }
        //    else if (num >= C_BIG)
        //    {
        //        return GetString(num, C_BIG, "C");
        //    }
        //    else if (num >= O_BIG)
        //    {
        //        return GetString(num, O_BIG, "O");
        //    }
        //    else if (num >= W_BIG)
        //    {
        //        return GetString(num, W_BIG, "W");
        //    }
        //    else if (num >= F_BIG)
        //    {
        //        return GetString(num, F_BIG, "F");
        //    }
        //    else if (num >= D_BIG)
        //    {
        //        return GetString(num, D_BIG, "D");
        //    }
        //    else if (num >= Z_BIG)
        //    {
        //        return GetString(num, Z_BIG, "Z");
        //    }
        //    else if (num >= X_BIG)
        //    {
        //        return GetString(num, X_BIG, "X");
        //    }
        //    else if (num >= V_BIG)
        //    {
        //        return GetString(num, V_BIG, "V");
        //    }
        //    else if (num >= S_BIG)
        //    {
        //        return GetString(num, S_BIG, "S");
        //    }
        //    else if (num >= L_BIG)
        //    {
        //        return GetString(num, L_BIG, "L");
        //    }
        //    else if (num >= H_BIG)
        //    {
        //        return GetString(num, H_BIG, "H");
        //    }
        //    else if (num >= G_BIG)
        //    {
        //        return GetString(num, G_BIG, "G");
        //    }
        //    else if (num >= N_BIG)
        //    {
        //        return GetString(num, N_BIG, "N");
        //    }
        //    else if (num >= R_BIG)
        //    {
        //        return GetString(num, R_BIG, "R");
        //    }
        //    else if (num >= E_BIG)
        //    {
        //        return GetString(num, E_BIG, "E");
        //    }
        //    else if (num >= P_BIG)
        //    {
        //        return GetString(num, P_BIG, "P");
        //    }
        //    else if (num >= U_BIG)
        //    {
        //        return GetString(num, U_BIG, "U");
        //    }
        //    else if (num >= Y_BIG)
        //    {
        //        return GetString(num, Y_BIG, "Y");
        //    }
        //    else if (num >= Q_BIG)
        //    {
        //        return GetString(num, Q_BIG, "Q");
        //    }
        //    else if (num >= TRILLION)
        //    {
        //        return GetString(num, TRILLION, "T");
        //    }
        //    else if (num >= BILLION)
        //    {
        //        return GetString(num, BILLION, "B");
        //    }
        //    else if (num >= MILLION)
        //    {
        //        return GetString(num, MILLION, "M");
        //    }
        //    else if (num >= THOUSAND)
        //    {
        //        return GetString(num, THOUSAND, "K");
        //    }
        //    else
        //    {
        //        return num.ToString();
        //    }
        //}

        static string GetString(BigInteger num, BigInteger index, string sympol)
        {
            int afterDot = int.Parse(((num % index) / (index / 100)).ToString());
            return string.Format("{0}.{1:D2}{2}", num / index, afterDot, sympol);
        }
        //public static string FormatBigBonus(BigInteger num)
        //{
        //    if (num >= A_BIG)
        //    {
        //        return GetBonusString(num, A_BIG, "A");
        //    }
        //    else if (num >= J_BIG)
        //    {
        //        return GetBonusString(num, J_BIG, "J");
        //    }
        //    else if (num >= I_BIG)
        //    {
        //        return GetBonusString(num, I_BIG, "I");
        //    }
        //    else if (num >= C_BIG)
        //    {
        //        return GetBonusString(num, C_BIG, "C");
        //    }
        //    else if (num >= O_BIG)
        //    {
        //        return GetBonusString(num, O_BIG, "O");
        //    }
        //    else if (num >= W_BIG)
        //    {
        //        return GetBonusString(num, W_BIG, "W");
        //    }
        //    else if (num >= F_BIG)
        //    {
        //        return GetBonusString(num, F_BIG, "F");
        //    }
        //    else if (num >= D_BIG)
        //    {
        //        return GetBonusString(num, D_BIG, "D");
        //    }
        //    else if (num >= Z_BIG)
        //    {
        //        return GetBonusString(num, Z_BIG, "Z");
        //    }
        //    else if (num >= X_BIG)
        //    {
        //        return GetBonusString(num, X_BIG, "X");
        //    }
        //    else if (num >= V_BIG)
        //    {
        //        return GetBonusString(num, V_BIG, "V");
        //    }
        //    else if (num >= S_BIG)
        //    {
        //        return GetBonusString(num, S_BIG, "S");
        //    }
        //    else if (num >= L_BIG)
        //    {
        //        return GetBonusString(num, L_BIG, "L");
        //    }
        //    else if (num >= H_BIG)
        //    {
        //        return GetBonusString(num, H_BIG, "H");
        //    }
        //    else if (num >= G_BIG)
        //    {
        //        return GetBonusString(num, G_BIG, "G");
        //    }
        //    else if (num >= N_BIG)
        //    {
        //        return GetBonusString(num, N_BIG, "N");
        //    }
        //    else if (num >= R_BIG)
        //    {
        //        return GetBonusString(num, R_BIG, "R");
        //    }
        //    else if (num >= E_BIG)
        //    {
        //        return GetBonusString(num, E_BIG, "E");
        //    }
        //    else if (num >= P_BIG)
        //    {
        //        return GetBonusString(num, P_BIG, "P");
        //    }
        //    else if (num >= U_BIG)
        //    {
        //        return GetBonusString(num, U_BIG, "U");
        //    }
        //    else if (num >= Y_BIG)
        //    {
        //        return GetBonusString(num, Y_BIG, "Y");
        //    }
        //    else if (num >= Q_BIG)
        //    {
        //        return GetBonusString(num, Q_BIG, "Q");
        //    }
        //    else if (num >= TRILLION)
        //    {
        //        return GetBonusString(num, TRILLION, "T");
        //    }
        //    else if (num >= BILLION)
        //    {
        //        return GetBonusString(num, BILLION, "B");
        //    }
        //    else if (num >= MILLION)
        //    {
        //        return GetBonusString(num, MILLION, "M");
        //    }
        //    else if (num >= THOUSAND)
        //    {
        //        return GetBonusString(num, THOUSAND, "K");
        //    }
        //    else
        //    {
        //        return num.ToString();
        //    }
        //}

        //static string GetBonusString(BigInteger num, BigInteger index, string sympol)
        //{
        //    //int afterDot = int.Parse(((num % index) / (index / 100)).ToString());
        //    int dot = RandomHelper.Range(1, 9);
        //    return string.Format("{0}.{1:D1}{2}", num / index, dot, sympol);
        //}
        public static string TurnLine(string origin)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return "";
            }
            string result = origin.Replace("~", "\n");
            return result;
        }

        public static string TurnLineFromLanguageTable(string origin)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return "";
            }
            string result = TDLanguageTable.Get(origin).Replace("~", "\n");
            return result;
        }

        public static string[] GetStringArror(string origin)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return null;
            }
            string[] toSpilt = { "|" };
            return origin.Split(toSpilt, StringSplitOptions.RemoveEmptyEntries);
        }

        //=============================
        private static string[] _suffixes;


        static StringHelper()
        {
            InitSuffixes();
        }
        private static void InitSuffixes()
        {
            _suffixes = new string[0x52];
            _suffixes[0] = "K";
            _suffixes[1] = "M";
            _suffixes[2] = "B";
            _suffixes[3] = "T";
            for (int i = 0; i < "abcdefghijklmnopqrstuvwxyz".Length; i++)
            {
                _suffixes[(i + 3) + 1] = "a" + "abcdefghijklmnopqrstuvwxyz"[i];
                _suffixes[((i + 3) + 1) + 0x1a] = "b" + "abcdefghijklmnopqrstuvwxyz"[i];
                _suffixes[((i + 3) + 1) + 0x34] = "c" + "abcdefghijklmnopqrstuvwxyz"[i];
            }
        }
        public static double Round(double d, int digits)
        {
            if (d <= 0.0)
            {
                return d;
            }
            double num = Math.Pow(10.0, Math.Floor(Math.Log10(Math.Abs(d))) + 1.0);
            return (num * Math.Round((double) (d / num), digits));
        }

        private static string Minify(double num)
        {
            for (int i = 0x52; i > 0; i--)
            {
                if (num >= Math.Pow(10.0, (double) (3 * i)))
                {
                    double num3 = num / Math.Pow(10.0, (double) (3 * i));
                    return (num3.ToString("0.##", CultureInfo.InvariantCulture) + _suffixes[i - 1]);
                }
            }
            return num.ToString(CultureInfo.InvariantCulture);
        }

        public static string MinifyFormat(this double num)
        {
            double num2 = Round(num, 3);
            return Minify(num2);
        }

        public static string FormatTime(int sum)
        {
            int minuteSum = sum / 60;
            int sec = sum % 60;
            return string.Format("{0:00}{1}{2:00}", minuteSum, ":", sec);
        }

        public static string FormatWeight(float weight)
        {
            if (weight > 999)
            {
                return MinifyFormat(weight/1000) + "t";
            }
            else
            {
                return weight.ToString("F1") + "kg";
            }
        }

        public static string GetRomaNum(int i)
        {
            if (i == 1)
            {
                return "I";
            }
            else if(i==2)
            {
                return "II";
            }
            else if (i==3)
            {
                return "III";
            }
            else if (i==4)
            {
                return "IV";
            }
            else if (i==5)
            {
                return "V";
            }else if (i==6)
            {
                return "VI";
            }
            else if (i==7)
            {
                return "VII";
            }
            else if (i==8)
            {
                return "VIII";
            }

            return i.ToString();
        }

        public static T ParseStringToEnum<T>(string str)
        {
            return (T)System.Enum.Parse(typeof(T), str);
        }
    }
}