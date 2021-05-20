using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using Qarth;
using GameWish.Game;
using System;

namespace UnityEngine.UI
{
    public enum ButtonAnimeType
    {
        None,
        Scale,
        Shake,
    }
    public enum SoundType
    {

        Default,
        Mute,
        Sound_ClickSummon,
        Sound_GetMoney,
        Sound_NoFood,
        Sound_OccupySuccess,
        Sound_PowerUp,
        Sound_SoldierDead,
        Sound_UpgradeSolider,
        Sound_SummonGiant,
        Sound_UseSoldier,
        Sound_UnlockNewSoldier,
    }


    [AddComponentMenu("UI/UIBtn", 30)]
    public class UIButton : Button
    {


        [Header("点击缩放")]
        public float m_ClickScale = 1.05f;

        public ButtonAnimeType m_AnimeType = ButtonAnimeType.Scale;
        public SoundType m_SoundType = SoundType.Default;
        public bool IsNeedOutChange = true;
        public bool m_IsSoundEnable = true;

        private Action m_ClickDownAnimeMethod;
        private Action m_ClickUpAnimeMethod;
        private string m_ClickSound = "";

        private float startSacle = 1;
        private Vector3 startPosition = Vector3.zero;
        private Tweener m_Tweener;

        private string m_DefaultClickSound = Define.DEFAULT_SOUND;
        public string ClickSoundName
        {
            get
            {

                if (m_SoundType != SoundType.Default && m_SoundType != SoundType.Mute)
                {
                    m_ClickSound = m_SoundType.ToString();
                }
                else
                {
                    m_ClickSound = "";
                }

                return m_ClickSound;
            }
        }


        public bool isSoundEnable
        {
            get { return m_IsSoundEnable; }
            set { m_IsSoundEnable = value; }
        }


        protected override void Start()
        {
            startSacle = transform.localScale.x;
            startPosition = transform.localPosition;
            ChangeBtnAnimeState();

        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            //transform.DOScale(startSacle * m_ClickScale, 0.2f);
            m_ClickDownAnimeMethod?.Invoke();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            //  transform.DOScale(startSacle, 0.2f);
            m_ClickUpAnimeMethod?.Invoke();

        }
        protected override void OnEnable()
        {
            transition = Transition.None;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {

            base.OnPointerUp(eventData);
            if (IsNeedOutChange)
            {
                transform.DOScale(startSacle, 0.2f);
            }


        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            //播放声音
            PlayButtonSound();
            //播放特效
            PlayButtonEffect();

            base.OnPointerClick(eventData);

        }


        protected void PlayButtonSound()
        {
            if (isSoundEnable)
            {
                if (!string.IsNullOrEmpty(ClickSoundName))
                {
                    AudioMgr.S.PlaySound(ClickSoundName);
                }
                else
                {
                    if (m_SoundType != SoundType.Mute)
                    {
                        AudioMgr.S.PlaySound(m_DefaultClickSound);
                    }
                    //播放默认音效 或者不播放
                }
            }
        }

        protected void PlayButtonEffect()
        {

        }

        #region PublicMethod
        public void ChangeBtnAnimeState(ButtonAnimeType animeType = ButtonAnimeType.None)
        {
            if (animeType != ButtonAnimeType.None)
            {
                m_AnimeType = animeType;
                m_Tweener = null;
            }

            switch (m_AnimeType)
            {
                case ButtonAnimeType.None:
                    break;
                case ButtonAnimeType.Scale:
                    m_ClickDownAnimeMethod = () => { transform.DOScale(startSacle * m_ClickScale, 0.2f); };
                    m_ClickUpAnimeMethod = () => { transform.DOScale(startSacle, 0.2f); };
                    break;
                case ButtonAnimeType.Shake:

                    m_ClickDownAnimeMethod = () =>
                    {
                        if(m_Tweener != null)
                        {
                            m_Tweener.Kill();
                        }
                        transform.localPosition = startPosition;

                        m_Tweener = transform.DOPunchPosition(new Vector3(10, 0, 0), 0.1f).OnComplete(() =>
                        {
                            transform.DOPunchPosition(new Vector3(-10, 0, 0), 0.1f).OnComplete(()=> {
                                transform.localPosition = startPosition;
                            }
);
                        });

                    };
                    m_ClickUpAnimeMethod = null;
                    break;
                default:
                    break;
            }
        }

        #endregion



    }
}

