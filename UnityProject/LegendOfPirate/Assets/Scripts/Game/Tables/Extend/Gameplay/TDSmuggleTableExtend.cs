using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using System.Linq;

namespace GameWish.Game
{
    public partial class TDSmuggleTable
    {
        static void CompleteRowAdd(TDSmuggle tdData, int rowCount)
        {

        }

        public static SmuggleUnitConfig GetConfigById(int id) 
        {
            var data = m_DataList.FirstOrDefault(d => d.id == id);
            return new SmuggleUnitConfig(data.id, data.addName, data.bannerName, data.time, data.award, data.coefficient);
        }
    }

    public class SmuggleUnitConfig
    {
        public int orderId;
        public string addressName;
        public string spriteName;
        public int time;
        public int rewardId;
        public float coefficient;

        public SmuggleUnitConfig(int orderId, string addressName, string spriteName, int time, int rewardId, float confficientString)
        {
            this.orderId = orderId;
            this.addressName = addressName;
            this.spriteName = spriteName;
            this.time = time;
            this.rewardId = rewardId;
            this.coefficient = confficientString;
        }
    }
}