/*
设计图Image
提供了 
剧中 半透明
外面 不透明

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GFrame
{
    public class DesignImage : Image
    {
        protected override void Start()
        {
            base.Start();
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
                gameObject.SetActive(false);
#endif
        }
    }
}
