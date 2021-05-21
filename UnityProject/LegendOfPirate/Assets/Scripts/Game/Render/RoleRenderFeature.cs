using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameWish.Game
{
    public class RoleRenderFeature : ScriptableRendererFeature
    {

        private RoleRenderPass m_Pass = null;

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            if (cameraData.renderType == CameraRenderType.Base)
            {
                renderer.EnqueuePass(m_Pass);
            }
        }

        public override void Create()
        {
            m_Pass = new RoleRenderPass();
        }


    }

    public class RoleRenderPass : ScriptableRenderPass
    {

        public RoleRenderPass()
        {
            this.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
        }

        private const string NameOfCommandBuffer = "Role";

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(NameOfCommandBuffer);
            try
            {
                cmd.Clear();

                foreach (var group in GPUInstanceMgr.S.renderGroupLst)
                {
                    group.UpdateMaterialProperties();
                    cmd.DrawMeshInstancedProcedural(group.drawMesh, 0, group.material, 0, 5, group.mpb);
                }

                context.ExecuteCommandBuffer(cmd);
            }

            finally
            {
                cmd.Release();
            }
        }
    }

}