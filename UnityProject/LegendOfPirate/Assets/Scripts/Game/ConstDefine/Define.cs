using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class Define
    {
        public const int DEFAULT_COIN_NUM = 0;
        public const int DEFAULT_DIAMOND_NUM = 0;
        public const int DEFAULT_TIP_NUM = 5;

        public const string DEFAULT_SOUND = "";
        public const string SOUND_DEFAULT_SOUND = "Sound_ButtonClick";
        public const string SOUND_BUTTON_CLICK = "Sound_ButtonClick";
        public const string SOUND_BLOCK_UPGRADE = "Sound_LevelUp"; 
        public const string SOUND_PANEL_CLOSE = "Sound_Close";    
        public const string SOUND_POSITIVE_EFFECT = "Sound_Positive"; 
        public const string SOUND_EVOLVE = "Sound_Evolve";

        public const string NAME_SPACE_PREFIX = "GameWish.Game.";
        //offline
        public const int OFFLINE_MAX_TIME = 120;
        public const int OFFLINE_MIN_TIME = 120;
        public const int OFFLINE_RATE_MIN = 2;
        public const int OFFLINE_RATE_MAX = 6;

        //offline earning
        public const int OFFLINE_MONEY_RATE = 1;
        public const int OFFLINE_EXP_RATE = 30;
        public const int OFFLINE_DIAMOND_COST = 100;

        //rate record
        public const string RATE_RECORD = "rate";

       

        public const string AD_PLACEMENT_INTER = "";

        public const int AD_MAX_TIME = 120;


    }
}
