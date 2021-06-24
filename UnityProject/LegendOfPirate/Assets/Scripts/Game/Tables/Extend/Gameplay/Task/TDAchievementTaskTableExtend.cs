using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDAchievementTaskTable
    {
        static void CompleteRowAdd(TDAchievementTask tdData, int rowCount)
        {

        }
        public static int[] GetTargetCountList(int key)
        {
            string m_Target = TDAchievementTaskTable.GetData(key).taskCount;
            string[] temp = m_Target.Split(';');
            int[] m_TargetList = new int[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                m_TargetList[i] = int.Parse(temp[i]);
            }
            return m_TargetList;
        }
    }
}