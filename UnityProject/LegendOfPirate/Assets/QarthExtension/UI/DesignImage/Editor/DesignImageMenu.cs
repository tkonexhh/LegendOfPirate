using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GFrame.Editor
{
    public class DesignImageMenu : MonoBehaviour
    {
        [MenuItem("GameObject/UI/DesignImage")]
        static void CreatDesignImage()
        {
            // CreateUI(Image);

            GameObject go = new GameObject("<DesignImage>", typeof(DesignImage));
            go.transform.SetParent(Selection.activeTransform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            Selection.activeGameObject = go;

            Undo.RegisterCreatedObjectUndo(go, "CreatDesignImage");
        }
    }
}