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
            modelMonoReference = renderObject.GetComponent<RoleModelMonoReference>();
            PlayAnim(BattleDefine.ROLEANIM_IDLE);
        }

        public override void OnDestroy()
        {
            BattleMgr.DestroyImmediate(renderObject);
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