using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public struct ResPair
    {
        public int resId;
        public int resCount;

        public ResPair(int resid, int rescount)
        {
            resCount = rescount;
            resId = resid;

        }

        public static List<ResPair> StringToResPairLst(string rawString, char Spliter1 = ';', string Spliter2 = "|")
        {
            List<ResPair> ret = new List<ResPair>();
            var resPairStrings = rawString.Split(Spliter1);
            foreach (var resPairString in resPairStrings)
            {
                var resPairCount = Helper.String2IntArray(resPairString, Spliter2);
                ret.Add(new ResPair(resPairCount[0], resPairCount[1]));
            }
            return ret;
        }
    }
}