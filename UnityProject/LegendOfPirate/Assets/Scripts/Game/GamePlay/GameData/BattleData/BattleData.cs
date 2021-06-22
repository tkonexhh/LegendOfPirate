using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleData : IDataClass
    {
        public BattleFieldData fieldData;
        public override void InitWithEmptyData()
        {
            fieldData = new BattleFieldData();
        }

        public override void OnDataLoadFinish()
        {

        }
    }


    public class BattleFieldData : IDataClass
    {
        public List<BattleFieldInfoData> battleFields = new List<BattleFieldInfoData>();
        private Dictionary<int, BattleFieldInfoData> m_BattleFieldMap = new Dictionary<int, BattleFieldInfoData>();

        public override void InitWithEmptyData()
        {
            for (int y = 0, index = 0; y < BattleDefine.BATTLE_HEIGHT; y++)
            {
                for (int x = 0; x < BattleDefine.BATTLE_WIDTH; x++)
                {
                    battleFields.Add(new BattleFieldInfoData() { ID = index, roleID = -1 });
                    index++;
                }
            }
        }

        public override void OnDataLoadFinish()
        {
            for (int i = 0; i < battleFields.Count; i++)
            {
                m_BattleFieldMap.Add(i, battleFields[i]);
            }
        }

        // public 

    }

    public class BattleFieldInfoData
    {
        public int ID;
        public int roleID = -1;
    }
}