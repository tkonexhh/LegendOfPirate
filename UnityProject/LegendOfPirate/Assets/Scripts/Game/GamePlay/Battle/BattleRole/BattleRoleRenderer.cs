using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : BattleRoleComponent
    {
        private PlayablesAnimation animator;
        public RoleModelMonoReference modelMonoReference { get; private set; }
        public string prefabName;
        public GameObject renderObject;
        public BattleRoleRenderer(BattleRoleController controller) : base(controller) { }


        private Stack<string> m_AnimationStack = new Stack<string>();

        public override void OnInit()
        {
            renderObject = BattleMgr.S.Pool.GetGameObject(prefabName);//GameObject.Instantiate(prefab);
            renderObject.transform.SetParent(controller.transform);
            renderObject.transform.localPosition = Vector3.zero;
            renderObject.transform.localRotation = Quaternion.identity;
            animator = renderObject.GetComponent<PlayablesAnimation>();
            animator.onAnimComplete += OnAnimComplete;
            modelMonoReference = renderObject.GetComponent<RoleModelMonoReference>();
            PushAnim(BattleDefine.ROLEANIM_IDLE);
        }

        public override void OnDestroy()
        {
            BattleMgr.DestroyImmediate(renderObject);
        }

        private void OnAnimComplete()//当一个不循环动画播放完成后
        {
            // Debug.LogError("Complete");
            m_AnimationStack.Pop();
            PlayAnim(m_AnimationStack.Peek());
        }


        public void PushAnim(string animName)
        {
            m_AnimationStack.Push(animName);
            PlayAnim(animName);
        }

        public void PushAnimFade(string animName, float fadeTime)
        {
            m_AnimationStack.Push(animName);
            CrossFadeAnim(animName, fadeTime);
        }

        public void PlayAnim(string animName)
        {
            animator.Play(animName);
        }

        public void CrossFadeAnim(string animName, float fadeTime)
        {
            animator.CrossFade(animName, fadeTime);
        }
    }

}