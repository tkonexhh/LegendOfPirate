using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDLevelConfigTable
    {
        static void CompleteRowAdd(TDLevelConfig tdData)
        {

        }

        public static int GetTime(int level)
        {
            TDLevelConfig config = GetLevel(level);
            return config.time;
        }

        public static int GetModelCount(int level)
        {
            TDLevelConfig config = GetLevel(level);
            return config.modelCount;
        }

        public static bool HasModelUnlocked(int level)
        {
            TDLevelConfig config = GetLevel(level);
            return config.unlockNewModel == 1;
        }

        private static TDLevelConfig GetLevel(int level)
        {
            level = Mathf.Clamp(level, 1, 20);
            TDLevelConfig config = m_DataList[level - 1];

            return config;
        }
    }
}