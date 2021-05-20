using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public enum ConstType
    {
        MUSIC_BG = 0,
        SOUND_BUTTONCLICK=1,
        SOUND_TABSWITCH,
        SOUND_PLAYERLEVELUP,
        SOUND_EARNGOLDSINGLE,
        SOUND_PUTBOATONSEA,
        MUSIC_MONSTERBATTLE,
        SOUND_MONSTERAPPEAR,
        SOUND_MONSTERHURT,
        SOUND_SEAATMOSPHERE,
        /// <summary>
        /// 回收船只音效
        /// </summary>
        SOUND_RECYCLESHIP,
        SOUND_MONSTERFLEE,
        /// <summary>
        /// 解锁船只使用
        /// </summary>
        SOUND_UNLOCKNSHIP,
        SOUND_WARNING,//所有不满足条件的点击均用此效果，例如钱不够，水晶不足
        SOUND_UNLOCKLAND,//解锁岛屿播放一次，播放时机从解锁的UI隐藏时延迟0.5s
        SOUND_HIT,
        SOUND_SEAGULL1,
        SOUND_SEAGULL2,
        SOUND_DEFAULT_BUTTON,
    }

    public class AudioUnitID
    {
        public static int MUSIC_BGID;
        public static int MUSIC_MONSTERBATTLE;
    }

}
