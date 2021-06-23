using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDEquipmentSynthesisConfig
    {
        public void Reset()
        {

        }
        public List<ResPair> GetEquipmentResPairs()
        {
            List<ResPair> ret=new List<ResPair>();
            var respairString= m_MakeRes.Split(';');
            foreach (var item in respairString) 
            {
                var resMsgString= item.Split('|');
                ResPair resPair = new ResPair(Helper.String2Int(resMsgString[0]), Helper.String2Int(resMsgString[1]));
                ret.Add(resPair);
            }
            return ret;
        }
    }
}