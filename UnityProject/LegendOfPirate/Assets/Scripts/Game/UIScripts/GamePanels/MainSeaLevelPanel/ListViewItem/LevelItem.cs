
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class LevelItem : MonoBehaviour
    {
        public int curLevelId;
        public void OnUIInit(MainSeaLevelPanel mainSeaLevelPanel, TDMarinLevelConfig levelData)
        {
            curLevelId = levelData.level;
            m_TmpLevel.text = String.Format("LEVEL {0}", levelData.level);
            // Debug.Log(m_TmpLevel.text);
        }

    }
}