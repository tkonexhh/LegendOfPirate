using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class RoleRenderComponent : EntityRenderComponent
    {
        protected GameObject gameObject;
        protected Transform transform;
        protected FootmanCellItem m_RenderInfo;
        string soName = "Enemy1ConfigSO";

        public override void InitComponent(EntityBase owner)
        {
            base.InitComponent(owner);
            gameObject = new GameObject("Role_" + m_Owner.EntityID);
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);
            transform.localPosition = Random.insideUnitSphere * Random.Range(1.0f, 3.0f);
            m_RenderInfo = new FootmanCellItem();


            if (GPUInstanceMgr.S.HasRenderGroup(soName))
            {
                GPUInstanceMgr.S.GetRenderGroup(soName).AddCellItem(m_RenderInfo);
            }
            else
            {
                var config = BattleMgr.S.loader.LoadSync(soName) as RoleConfigSO;
                AnimDataInfo animDataInfo = JsonUtility.FromJson<AnimDataInfo>(config.animInfoText.text);
                FootmanGroup group = new FootmanGroup(config.mesh, config.material, animDataInfo);
                GPUInstanceMgr.S.AddRenderGroup(group).AddCellItem(m_RenderInfo);
            }

            m_RenderInfo.Play("Idle", true);
        }

        public override void Tick(float deltaTime)
        {
            m_RenderInfo.rotation = transform.rotation;
            m_RenderInfo.pos = transform.position;
        }

        public override void OnDestory()
        {
            GPUInstanceMgr.S.GetRenderGroup(soName).RemoveCellItem(m_RenderInfo);

        }
    }

}