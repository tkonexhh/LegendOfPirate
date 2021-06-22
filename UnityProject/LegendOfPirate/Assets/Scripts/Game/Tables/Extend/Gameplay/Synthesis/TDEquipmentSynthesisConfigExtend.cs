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
        public List<ForgeRoomEquipmentResPair> GetEquipmentResPairs()
        {
            List<ForgeRoomEquipmentResPair> ret=new List<ForgeRoomEquipmentResPair>();
            var respairString= m_MakeRes.Split(';');
            foreach (var item in respairString) 
            {
                var resMsgString= item.Split('|');
                ForgeRoomEquipmentResPair resPair = new ForgeRoomEquipmentResPair(Helper.String2Int(resMsgString[0]), Helper.String2Int(resMsgString[1]));
                ret.Add(resPair);
            }
            return ret;
        }
    }
}