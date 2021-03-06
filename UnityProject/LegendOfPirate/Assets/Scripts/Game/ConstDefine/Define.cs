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
        public const int REFRESH_TIME_POINT = 6;
        public const int INT_NUMBER_ZERO = 0;
        public const string STRING_NUMBER_ZERO = "0";

        public const string DEFAULT_SOUND = "";
        public const string DEFAULT_NULL = "";
        public const string SYMBOL_SLASH = "/";
        public const string SYMBOL_PERCENT= "%";
        public const string DOLLAR_SIGN= "$";


        public const string NAME_SPACE_PREFIX = "GameWish.Game.";

        //rate record
        public const string RATE_RECORD = "rate";

        public const string GAME_LAYER = "Game";

        // Prefab Name
        public const string SEA_PREFAB = "CartoonSea";
        public const string SHIP_PREFAB = "Ship";
        public const string ROLE_PREFAB = "ShipRole";

        public const int INPUT_SORTING_ORDER_DEFAULT = 0;
        public const int INPUT_SORTING_ORDER_SHIP_UNIT = 5;
        public const int INPUT_SORTING_ORDER_SHIP_ROLE = 10;

        // Ship Unit Const
        public const int TRAINING_ROOM_MAX_SLOT = 9;
        public const int LIBRARY_ROOM_MAX_SLOT = 9;
        public const int PROCESSING_ROOM_MAX_SLOT = 6;
        public const int KITCHEN_MAX_SLOT = 6;
        public const int LABORATORY_MAX_SLOT = 6;

        //Ship Unit Max Level
        public const int TRAINING_ROOM_MAX_LEVEL = 9;

        //Ship Unit Default
        public const int PROCESSING_ROOM_DEFAULT_SLOT_COUNT = 4;
        public const int KITCHEN_DEFAULTS_SLOT_COUNT = 4;
        public const int LABORATORY_DEFAULT_SLOT_COUNT = 4;
        
        //Inventroy
        public const int INVENTORY_ITEM_MAX_COUNT = 9999;

        //Role
        public const int ROLEGET_NEED_SPIRIT_COUNT = 100;

    }
}
