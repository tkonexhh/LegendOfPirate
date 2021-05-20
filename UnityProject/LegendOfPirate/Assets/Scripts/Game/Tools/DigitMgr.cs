using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;
namespace GameWish.Game
{
    public enum DigitSign:byte
    {
        K=0,
        M=1,
        G=2,
        T=3,
        P=4,
        E=5,
        Z=6,
        Y=7,
        B=8,
        N=9,
        D=10,
        None,
    }

    public class DigitMgr : TSingleton<DigitMgr>
    {
        public override void OnSingletonInit()
        {
            
        }

        public string GetShowDigitSumSign(long digit)
        {
            int digitSum = GetShowDigitSum(digit);
            string str = GetShowDigitSignByLength(digit.ToString().Length);
            return string.Concat(digitSum,str);
        }

        public string GetShowFormatDigitSumSign(long digit)
        {
            if (digit < 0)
                Log.e("digit  ="+digit);
            int digitSum = GetShowDigitSum(digit);
            string str = GetShowDigitSignByLength(digit.ToString().Length);
            return string.Format("{0:N0}{1}",digitSum, str);
        }

        public int GetShowDigitSum(long digit)//if(str.length>19 ）  log.e("不存在的")
        {
            string str = digit.ToString();
            int tempValue=0;
            if(str.Length>5)
            {
              //  int sublength = str.Length % 3 == 0 ? 3 : str.Length % 3;
                tempValue =  int.Parse(str.Substring(0,(3+str.Length % 3)));
            }else
            {
                tempValue = (int)digit;
            }
            return tempValue;
        }

        public string GetShowDigitSignByLength(int digitLength)
        {
            if (digitLength <= 5)
            {
                return "";
            }
            else
            {
                int digitSignIndex = digitLength/3-2;//(digitLength-1)/3-2;
                return GetStrDigitSign((DigitSign)digitSignIndex);
            }
        }

        string GetStrDigitSign(DigitSign digitSign)//if（digitSign== DigitSign.E）  log.e("不存在的")
        {
           string str = "";
            switch (digitSign)
            {
                case DigitSign.K:
                    str = "K";
                    break;
                case DigitSign.M:
                    str = "M";
                    break;
                case DigitSign.G:
                    str = "G";
                    break;
                case DigitSign.T:
                    str = "T";
                    break;
                case DigitSign.P:
                    str = "P";
                    break;
                case DigitSign.E:
                    str = "E";
                    break;
                case DigitSign.Z:
                    str = "Z";
                    break;
                case DigitSign.Y:
                    str = "Y";
                    break;
                case DigitSign.B:
                    str = "B";
                    break;
                case DigitSign.N:
                    str = "N";
                    break;
                case DigitSign.D:
                    str = "D";
                    break;
                case DigitSign.None:
                    str = "";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        DigitSign GetStrDigitSignType(string strSign)//if（digitSign== DigitSign.E）  log.e("不存在的")
        {
            DigitSign digitSign = DigitSign.None;
            switch (strSign)
            {
                case "K":
                    digitSign = DigitSign.K;
                    break;
                case "M":
                    digitSign = DigitSign.M;
                    break;
                case "G":
                    digitSign = DigitSign.G;
                    break;
                case "T":
                    digitSign = DigitSign.T;
                    break;
                case "P":
                    digitSign = DigitSign.P;
                    break;
                case "E":
                    digitSign = DigitSign.E;
                    break;
                case "Z":
                    digitSign = DigitSign.Z;
                    break;
                case "Y":
                    digitSign = DigitSign.Y;
                    break;
                case "B":
                    digitSign = DigitSign.B;
                    break;
                case "N":
                    digitSign = DigitSign.N;
                    break;
                case "D":
                    digitSign = DigitSign.D;
                    break;
                case "":
                case null:
                    digitSign = DigitSign.None;
                    break;
                default:
                    Log.e("strSign = "+strSign);
                    break;
            }
            return digitSign;
        }

        /// <summary>
        /// 倒计时
        /// </summary>
        public string GetCountDownBySum(int sum)
        {
            int minuteSum = sum / 60;
            int sec = sum %60;
            return string.Format("{0:00}{1}{2:00}",minuteSum,":",sec);
        }

        public string GetInComeByFloatValue(float sum)
        {
            string str = string.Format("{0}{1:f1}","x ",sum);
            return str;
        }

        public string GetGameTimeByS(long s)
        {
            string str = "";
            TimeSpan ts = new TimeSpan(0,0,(int)s);
            str = string.Format("{0:D2}{1}{2:D2}{3}{4:D2}",(int)ts.TotalHours,":",ts.Minutes,":",ts.Seconds);
            return str;
        }

    }
}