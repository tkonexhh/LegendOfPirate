using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : Controller
    {
        private Transform m_Target;
        protected AnimedRenderCell m_RenderInfo;
        string soName = "Enemy1ConfigSO";



        #region IElement
        public override void OnInit()
        {
            // gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            // transform = gameObject.transform;
            // transform.SetParent(BattleMgr.S.transform);
            m_RenderInfo = new AnimedRenderCell();


            if (GPUInstanceMgr.S.HasRenderGroup(soName))
            {
                GPUInstanceMgr.S.GetRenderGroup(soName).AddRenderCell(m_RenderInfo);
            }
            else
            {
                var config = BattleMgr.S.loader.LoadSync(soName) as RoleConfigSO;
                AnimDataInfo animDataInfo = JsonUtility.FromJson<AnimDataInfo>(config.animInfoText.text);
                RenderGroup group = new AnimedRenderGroup(soName, config.mesh, config.material, animDataInfo);
                GPUInstanceMgr.S.AddRenderGroup(group).AddRenderCell(m_RenderInfo);
            }


        }

        public override void OnUpdate()
        {
            if (m_Target == null)
                return;
            m_RenderInfo.rotation = m_Target.rotation;
            m_RenderInfo.position = m_Target.position;
            m_RenderInfo.Update();
        }

        public override void OnDestroyed()
        {
            GPUInstanceMgr.S.GetRenderGroup(soName).RemoveRenderCell(m_RenderInfo);
            // GameObjectPoolMgr.S.Recycle(gameObject);
            m_RenderInfo = null;
            // transform = null;
        }

        public override void OnCacheReset()
        {
            base.OnCacheReset();
            OnDestroyed();
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
            ObjectPool<BattleRoleRenderer>.S.Recycle(this);
        }
        #endregion

        public void SetTarget(Transform transform)
        {
            m_Target = transform;
        }

        public void PlayAnim(string animName, bool loop)
        {
            m_RenderInfo.Play(animName, loop);
        }

        public void CrossFadeAnim(string animName, float fadeTime, bool loop)
        {
            m_RenderInfo.CrossFade(animName, fadeTime, loop);
        }
    }

}