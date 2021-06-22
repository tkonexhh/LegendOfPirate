using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPartSynthesisConfig
    {
        public void Reset()
        {

        }

        public List<ResPair> GetMakeResList() 
        {
            List<ResPair> ret = new List<ResPair>();
            var resPairString = m_MakeRes.Split(';');
            foreach (var item in resPairString) 
            {
                var resMsg = Helper.String2IntArray(item, "|");
                ret.Add(new ResPair(resMsg[0], resMsg[1]));
            }
            return ret;
        }
    }
}