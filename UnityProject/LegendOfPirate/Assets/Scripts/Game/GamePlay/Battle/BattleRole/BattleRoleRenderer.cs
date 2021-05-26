using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleRenderer : Controller
    {
        public GameObject gameObject;
        public Transform transform;
        protected AnimedRenderCell m_RenderInfo;
        string soName = "Enemy1ConfigSO";

        #region IElement
        public override void OnInit()
        {
            gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);
            transform.localPosition = Random.insideUnitSphere * Random.Range(1.0f, 3.0f);
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

            m_RenderInfo.Play("Idle", true);
        }

        public override void OnUpdate()
        {
            m_RenderInfo.rotation = transform.rotation;
            m_RenderInfo.position = transform.position;
            m_RenderInfo.Update();
        }

        public override void OnDestroyed()
        {
            GPUInstanceMgr.S.GetRenderGroup(soName).RemoveRenderCell(m_RenderInfo);
            GameObjectPoolMgr.S.Recycle(gameObject);
            m_RenderInfo = null;
            transform = null;

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
    }

}