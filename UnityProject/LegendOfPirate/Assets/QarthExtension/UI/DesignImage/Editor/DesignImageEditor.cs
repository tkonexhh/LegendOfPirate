using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GFrame.Editor
{
    [CustomEditor(typeof(DesignImage))]
    public class DesignImageEditor : UnityEditor.UI.ImageEditor
    {
        DesignImage designImage;
        protected override void OnEnable()
        {
            base.OnEnable();
            designImage = (DesignImage)target;
            designImage.raycastTarget = false;
            designImage.gameObject.name = "<DesignImage>";
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("居中半透明"))
            {
                designImage.transform.localPosition = Vector3.zero;
                designImage.color = new Color(1, 1, 1, 0.5f);
            }

            if (GUILayout.Button("外面不透明"))
            {
                designImage.transform.localPosition = new Vector3(900, 0, 0);
                designImage.color = new Color(1, 1, 1, 1);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("居中"))
            {
                designImage.transform.localPosition = Vector3.zero;
            }

            if (GUILayout.Button("外面"))
            {
                designImage.transform.localPosition = new Vector3(900, 0, 0);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("最上层"))
            {
                designImage.transform.SetAsFirstSibling();
            }

            if (GUILayout.Button("最下层"))
            {
                designImage.transform.SetAsLastSibling();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("半透明"))
            {
                designImage.color = new Color(1, 1, 1, 0.5f);
                // designImage;
            }

            if (GUILayout.Button("不透明"))
            {
                designImage.color = new Color(1, 1, 1, 1);
                // designImage.RecalculateClipping();
            }
            GUILayout.EndHorizontal();
        }


    }
}
