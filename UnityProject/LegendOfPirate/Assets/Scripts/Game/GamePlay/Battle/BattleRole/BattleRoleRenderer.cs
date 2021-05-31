using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : Controller
    {
        public Transform transform { get; set; }
        protected AnimedRenderCell m_RenderInfo;
        string soName = "Enemy1ConfigSO";



        #region IElement
        public override void OnInit()
        {
            m_RenderInfo = new AnimedRenderCell();
            m_RenderInfo.animLerp = 0;
            m_RenderInfo.Pause();


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
            if (transform == null)
                return;
            m_RenderInfo.rotation = transform.rotation;
            m_RenderInfo.position = transform.position;
            m_RenderInfo.Update();
        }

        public override void OnDestroyed()
        {
            GPUInstanceMgr.S.GetRenderGroup(soName).RemoveRenderCell(m_RenderInfo);
            m_RenderInfo = null;
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

        public void PlayAnim(string animName, bool loop = false)
        {
            m_RenderInfo.Play(animName, loop);
        }

        public void CrossFadeAnim(string animName, float fadeTime, bool loop = false)
        {
            //TODO 攻击动画播放结束 跳回idle状态
            m_RenderInfo.CrossFade(animName, fadeTime, loop);
        }
    }

}