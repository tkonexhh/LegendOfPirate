using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFoodSynthesisConfig
    {
        public void Reset()
        {

        }

        public List<ResPair> GetResPair() 
        {
            List<ResPair> ret = new List<ResPair>();
            var resPairStrings = m_MakeRes.Split(';');
            foreach (var resPairString in resPairStrings) 
            {
                var resPairArray = Helper.String2IntArray(resPairString, "|");
                ret.Add(new ResPair(resPairArray[0], resPairArray[1]));
            }
            return ret;
        }
        
    }
}