using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class BattleRoleAnimation : MonoBehaviour
    {

        public List<AnimationClip> clipsList;

        private PlayableGraph m_Graph;
        private AnimationMixerPlayable animationMixer;
        private AnimationClipPlayable prePlayable, currentPlayable;


        private void Awake()
        {
            m_Graph = PlayableGraph.Create("BattleRoleAnimation");
        }

        private void Start()
        {
            m_Graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            GraphVisualizerClient.Show(m_Graph);

            AnimationPlayableOutput animationOutput = AnimationPlayableOutput.Create(m_Graph, "Animation Output", GetComponent<Animator>());
            AudioPlayableOutput audioOutput = AudioPlayableOutput.Create(m_Graph, "Audio Output", GetComponent<AudioSource>());


            animationMixer = AnimationMixerPlayable.Create(m_Graph, 2, false);


            var layerMixer = AnimationLayerMixerPlayable.Create(m_Graph, 1);
            layerMixer.AddInput(animationMixer, 0, 1);

            animationOutput.SetSourcePlayable(layerMixer);
            Play("idle");

            m_Graph.Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Play("run");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Play("idle");
            }
        }

        private void OnDestroy()
        {
            m_Graph.Destroy();
        }

        public void Play(string name)
        {
            Play(clipsList.Find(c => c.name == name));
        }

        public void Play(AnimationClip clip)
        {
            DisconnectPlayables();

            prePlayable = currentPlayable;
            currentPlayable = AnimationClipPlayable.Create(m_Graph, clip);

            animationMixer.ConnectInput(0, currentPlayable, 0);
            animationMixer.ConnectInput(1, prePlayable, 0);

            animationMixer.SetInputWeight(0, 1);
            animationMixer.SetInputWeight(1, 0);
        }


        public void CrossFade(string name, float fadeLength)
        {

        }

        public void CrossFade(AnimationClip clip)
        {

        }

        void DisconnectPlayables()
        {
            m_Graph.Disconnect(animationMixer, 0);
            m_Graph.Disconnect(animationMixer, 1);
        }
    }

}