using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using Qarth;

namespace GameWish.Game
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class PlayablesAnimation : MonoBehaviour
    {
        public List<AnimationClip> clipsList;

        private PlayableGraph m_Graph;
        private AnimationMixerPlayable animationMixer;
        private AnimationClipPlayable prePlayable, currentPlayable;

        private bool m_Fading;
        private float m_FadingTime, m_FadingSpeed;
        public Run onAnimComplete;

        private void Awake()
        {
            m_Graph = PlayableGraph.Create("PlayablesAnimation");
            m_Graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            GraphVisualizerClient.Show(m_Graph);

            AnimationPlayableOutput animationOutput = AnimationPlayableOutput.Create(m_Graph, "Animation Output", GetComponent<Animator>());
            AudioPlayableOutput audioOutput = AudioPlayableOutput.Create(m_Graph, "Audio Output", GetComponent<AudioSource>());


            animationMixer = AnimationMixerPlayable.Create(m_Graph, 2, false);


            var layerMixer = AnimationLayerMixerPlayable.Create(m_Graph, 1);
            layerMixer.AddInput(animationMixer, 0, 1);

            animationOutput.SetSourcePlayable(layerMixer);
            // Play("idle");

            m_Graph.Play();
        }

        private void Update()
        {
            if (m_Fading)
            {
                m_FadingTime += Time.deltaTime * m_FadingSpeed;
                m_FadingTime = Mathf.Clamp01(m_FadingTime);
                animationMixer.SetInputWeight(0, m_FadingTime);
                animationMixer.SetInputWeight(1, 1 - m_FadingTime);
                if (m_FadingTime >= 1.0f)
                {
                    m_Fading = false;
                }
            }


            // currentPlayable.GetAnimationClip().isLooping
            //当前如果不是循环动画，播放完成了的话

            if (!currentPlayable.GetAnimationClip().isLooping && currentPlayable.GetTime() >= currentPlayable.GetAnimationClip().length)
            {

                if (onAnimComplete != null)
                    onAnimComplete();
            }
            // Debug.LogError(currentPlayable;
        }

        private void OnDestroy()
        {
            m_Graph.Destroy();
        }

        public float GetLength(string name)
        {
            var clip = clipsList.Find(c => c.name == name);
            return clip.length;
        }


        public void Play(string name, float speed = 0.1f)
        {
            Play(clipsList.Find(c => c.name == name), speed);
        }

        public void Play(AnimationClip clip, float speed = 1.0f)
        {
            DisconnectPlayables();

            prePlayable = currentPlayable;
            currentPlayable = AnimationClipPlayable.Create(m_Graph, clip);
            currentPlayable.SetSpeed(speed);

            animationMixer.ConnectInput(0, currentPlayable, 0);
            animationMixer.ConnectInput(1, prePlayable, 0);

            animationMixer.SetInputWeight(0, 1);
            animationMixer.SetInputWeight(1, 0);
        }


        public void CrossFade(string name, float fadeLength, float speed = 1.0f)
        {
            CrossFade(clipsList.Find(c => c.name == name), fadeLength);
        }

        public void CrossFade(AnimationClip clip, float fadeLength, float speed = 1.0f)
        {
            DisconnectPlayables();

            prePlayable = currentPlayable;
            currentPlayable = AnimationClipPlayable.Create(m_Graph, clip);
            currentPlayable.SetSpeed(speed);

            animationMixer.ConnectInput(0, currentPlayable, 0);
            animationMixer.ConnectInput(1, prePlayable, 0);

            animationMixer.SetInputWeight(0, 1);
            animationMixer.SetInputWeight(1, 0);
            m_Fading = true;
            m_FadingTime = 0;
            m_FadingSpeed = 1.0f / fadeLength;
        }


        void DisconnectPlayables()
        {
            m_Graph.Disconnect(animationMixer, 0);
            m_Graph.Disconnect(animationMixer, 1);
        }
    }

}