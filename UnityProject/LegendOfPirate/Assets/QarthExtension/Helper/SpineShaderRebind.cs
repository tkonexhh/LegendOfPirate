using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace GameWish.Game
{
	public class SpineShaderRebind : MonoBehaviour
	{
        [SerializeField]
        private bool m_NeedBindAgain = false;

        private bool m_IsBind = false;

        private IEnumerator Start()
        {
            yield return null;

            if (m_IsBind == false)
            {
                RebindShader();

                m_IsBind = true;
            }

            if (m_NeedBindAgain)
            {
                yield return new WaitForSeconds(2);

                RebindShader();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                RebindShader();
            }
        }

        public void RebindShader()
        {
            SkeletonAnimation[] spines = GetComponentsInChildren<SkeletonAnimation>();
            foreach (SkeletonAnimation spine in spines)
            {
                Material[] materials = spine.GetComponent<MeshRenderer>().materials;
                foreach (Material m in materials)
                {
                    var shaderName = m.shader.name;
                    var newShader = Shader.Find(shaderName);
                    if (newShader != null)
                    {
                        m.shader = newShader;
                    }
                    else
                    {
                        Debug.LogWarning("unable to refresh shader: " + shaderName + " in material " + m.name);
                    }
                }
            }
        }
    }
	
}