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
        public BattleRoleRenderer(BattleRoleController controller) : base(controller) { }

        public override void OnInit()
        {
            var gameObject = GameObject.Instantiate(prefab);
            gameObject.transform.SetParent(controller.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            animator = gameObject.GetComponent<BattleRoleAnimation>();
            PlayAnim(BattleDefine.ROLEANIM_IDLE);
        }

        public override void OnDestroy()
        {
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