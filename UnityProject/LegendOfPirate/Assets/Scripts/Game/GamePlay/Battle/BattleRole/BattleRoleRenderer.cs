using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : BattleRoleComponent
    {
        // public Transform transform { get; set; }
        // protected AnimedRenderCell m_RenderInfo;
        private BattleRoleAnimation animator;
        string soName = "Enemy1ConfigSO";
        public BattleRoleRenderer(BattleRoleController controller) : base(controller)
        {
            var config = BattleMgr.S.loader.LoadSync(soName) as RoleConfigSO;
            var gameObject = GameObject.Instantiate(config.prefab);
            gameObject.transform.SetParent(controller.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            animator = gameObject.GetComponent<BattleRoleAnimation>();
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