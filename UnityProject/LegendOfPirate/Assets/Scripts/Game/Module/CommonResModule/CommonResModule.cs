using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
    public class CommonResModule : AbstractModule
    {
        protected override void OnComAwake()
        {

            HolderCommondAudio();


            ResHolder.S.loader.LoadSync();

        }

        private void HolderCommondAudio()
        {
            //ResHolder.S.AddRes(TDConstTable.QueryString(ConstType.MUSIC_BG));
            SoundButton.defaultClickSound = TDConstTable.QueryString(ConstType.SOUND_BUTTONCLICK);
            
            //ResHolder.S.AddRes(TDConstTable.QueryString(ConstType.SOUND_CLICK_RIGHT));
            //ResHolder.S.AddRes(TDConstTable.QueryString(ConstType.SOUND_CLICK_WRONG));

            //ResHolder.S.AddRes(TDConstTable.QueryString(ConstType.SOUND_WIN));
            //ResHolder.S.AddRes(TDConstTable.QueryString(ConstType.SOUND_FAILED));


        }

    }
}
