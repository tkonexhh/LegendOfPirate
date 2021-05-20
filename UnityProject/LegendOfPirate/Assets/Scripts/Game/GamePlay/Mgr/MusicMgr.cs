using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    public class MusicMgr : TSingleton<MusicMgr>
    {
        private bool m_IsBgMusicPlaying = false;
        private string abTestBgMusicName = "test";

        public void PlayBgMusic()
        {
            m_IsBgMusicPlaying = true;
            //string bgMusicTest = AbTestActor.GetBgMusicChangeIndex();
            //if (bgMusicTest == "0")
            {
                AudioUnitID.MUSIC_BGID = AudioMgr.S.PlayBg(TDConstTable.QueryString(ConstType.MUSIC_BG));
            }
            //else if (bgMusicTest == "1")
            //{
            //    AudioUnitID.MUSIC_BGID = AudioMgr.S.PlayBg("Music_bg1");
            //}
            //else if (bgMusicTest == "2")
            //{
            //    AudioUnitID.MUSIC_BGID = AudioMgr.S.PlayBg("Music_bg2");
            //}

            AudioMgr.S.SetVolume(AudioUnitID.MUSIC_BGID, 0.0f);

        }

        public void StopBgMusic()
        {
            m_IsBgMusicPlaying = false;
            AudioMgr.S.Stop(AudioUnitID.MUSIC_BGID);
        }

        public void PauseBgMusic()
        {
            m_IsBgMusicPlaying = false;
            AudioMgr.S.Pause(AudioUnitID.MUSIC_BGID);
        }

        public void ResumeBgMusic()
        {
            m_IsBgMusicPlaying = true;
            AudioMgr.S.Resume(AudioUnitID.MUSIC_BGID);
        }
    }
}
