using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CombineMesh : MonoBehaviour
{
    private void Start()
    {
        //获取材质
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        Material[] materials = new Material[meshRenderers.Length];

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            materials[i] = meshRenderers[i].sharedMaterial;
        }

        //获取mesh，使用CombineInstance类是因为CombineMeshes方法的需要
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            //模型空间坐标转化为世界坐标
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //隐藏子物体
            meshFilters[i].gameObject.SetActive(false);
        }

        //合并材质
        transform.GetComponent<MeshRenderer>().sharedMaterials = materials;
        //合并网格
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combineInstances, false);
        transform.gameObject.SetActive(true);
    }
}

