using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : BattleRoleComponent
    {
        private BattleRoleAnimation animator;
        public GameObject prefab;
        public GameObject renderObject;
        public BattleRoleRenderer(BattleRoleController controller) : base(controller) { }

        public override void OnInit()
        {
            renderObject = GameObject.Instantiate(prefab);
            renderObject.transform.SetParent(controller.transform);
            renderObject.transform.localPosition = Vector3.zero;
            renderObject.transform.localRotation = Quaternion.identity;
            animator = renderObject.GetComponent<BattleRoleAnimation>();
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