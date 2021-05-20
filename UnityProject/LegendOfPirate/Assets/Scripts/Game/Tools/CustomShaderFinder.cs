using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class CustomShaderFinder : MonoBehaviour
    {
#if UNITY_EDITOR

        //private List<Material> m_Mats = new List<Material>();
        //private List<string> m_ShaderName = new List<string>();
        private List<Renderer> m_Renders = new List<Renderer>();

        private void OnEnable()
        {
            m_Renders.Clear();
            var trsChildren = GetChildTrans(transform);
            trsChildren.Add(transform);
            trsChildren.ForEach(trs =>
            {
                var renderer = trs.GetComponent<Renderer>();
                if (renderer)
                {
                    m_Renders.Add(renderer);
                }
            });

            for (int i = 0; i < m_Renders.Count; i++)
            {
                for (int j = 0; j < m_Renders[i].materials.Length; j++)
                {
                    m_Renders[i].materials[j].shader = Shader.Find(m_Renders[i].materials[j].shader.name);
                }
            }
        }

        List<Transform> GetChildTrans(Transform rootTrs)
        {
            List<Transform> childsTrs = new List<Transform>();
            for (int i = 0; i < rootTrs.childCount; i++)
            {
                if (rootTrs.GetChild(i).gameObject.activeSelf)
                {
                    childsTrs.Add(rootTrs.GetChild(i));
                    childsTrs.AddRange(GetChildTrans(rootTrs.GetChild(i)));
                }
            }
            return childsTrs;
        }
#endif
    }
}
