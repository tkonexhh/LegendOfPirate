using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class GPUInstanceGroup
    {
        private Mesh m_DrawMesh;
        private Material m_Mat;
        private AnimDataInfo m_AnimDataInfo;//文本中的信息
        protected MaterialPropertyBlock m_MatPropBlock;
        private ComputeBuffer _RoleBuffer;


        public Mesh drawMesh => m_DrawMesh;
        public Material material => m_Mat;
        public MaterialPropertyBlock mpb => m_MatPropBlock;
        public string name { get; private set; }

        // private int m_DrawCount;
        // private int m_DrawCapacity;
        // protected List<GPUInstanceCell> m_Cells;
        public static int GrassInfosID = Shader.PropertyToID("_AnimInfo");
        public GPUInstanceGroup(Mesh mesh, Material material, AnimDataInfo animDataInfo)
        {
            m_DrawMesh = mesh;
            m_Mat = material;
            m_AnimDataInfo = animDataInfo;
            // m_Cells = new List<GPUInstanceCell>();
            m_MatPropBlock = new MaterialPropertyBlock();
            _RoleBuffer = new ComputeBuffer(10, 76);

            // m_MatPropBlock.SetBuffer(GrassInfosID, _RoleBuffer);
            name = mesh.name;
        }

        public void AddCellItem(GPUInstanceCellItem cellItem)
        {
            // for (int i = 0; i < m_Cells.Count; i++)
            // {
            //     int index = m_Cells[i].IndexOf(null);
            //     if (index != -1)
            //     {
            //         m_Cells[i].Set(index, cellItem);
            //         if (cellItem != null)
            //             cellItem.cellIndex = m_Cells[i].CellIndex;
            //         // Debug.LogError("找到null，直接set:" + m_Cells[i].CellIndex);
            //         return;
            //     }
            // }

            // GPUInstanceCell cell;
            // //寻找合适的
            // if (m_DrawCount + 1 >= m_DrawCapacity - m_Cells.Count + 1)
            // {
            //     //创建新的Cell
            //     cell = CreateCell();
            // }
            // else
            // {
            //     cell = m_Cells[m_Cells.Count - 1];

            // }
            // if (cellItem != null)
            // {
            //     cellItem.cellIndex = cell.CellIndex;
            //     // Debug.LogError(cellItem.cellIndex);
            // }

            // cell.Add(cellItem);
            // m_DrawCount++;
        }

        public bool RemoveCellItem(GPUInstanceCellItem cellItem)
        {
            if (cellItem == null)
            {
                return true;
            }

            // int cellIndex = cellItem.cellIndex;
            // int index = m_Cells[cellIndex].IndexOf(cellItem);
            // if (index == -1)
            //     return false;
            // // Debug.LogError("Remove:" + index);
            // m_Cells[cellIndex].Set(index, null);
            // // m_DrawCount--;
            return true;
        }

        // public GPUInstanceCell CreateCell()
        // {
        //     m_DrawCapacity = GPUInstanceDefine.MAX_CAPACITY * (m_Cells.Count + 1);
        //     GPUInstanceCell cell = OnCreateCell();
        //     cell.CellIndex = m_Cells.Count;
        //     m_Cells.Add(cell);
        //     return cell;
        // }

        // public void Draw()
        // {
        //     for (int i = 0; i < m_Cells.Count; i++)
        //     {
        //         m_Cells[i].Draw();
        //     }
        // }

        public void UpdateMaterialProperties()
        {
            List<AnimInfo> animInfos = new List<AnimInfo>();
            for (int i = 0; i < 10; i++)
            {
                AnimInfo animInfo = new AnimInfo();
                animInfo.animLerp = 0;
                animInfo.animRate1 = 0.2f;
                animInfo.animRate2 = 0;
                animInfo.trs = Matrix4x4.TRS(UnityEngine.Random.insideUnitSphere, Quaternion.identity, Vector3.one);
                animInfos.Add(animInfo);
            }
            _RoleBuffer = new ComputeBuffer(10, 76);
            _RoleBuffer.SetData(animInfos);
            m_MatPropBlock.SetBuffer(GrassInfosID, _RoleBuffer);
        }

        // #region override

        // protected virtual GPUInstanceCell OnCreateCell()
        // {
        //     return new GPUInstanceCell(this);
        // }
        // #endregion

    }

    public struct AnimInfo
    {
        public Matrix4x4 trs;
        public float animRate1;
        public float animRate2;
        public float animLerp;
    };
}