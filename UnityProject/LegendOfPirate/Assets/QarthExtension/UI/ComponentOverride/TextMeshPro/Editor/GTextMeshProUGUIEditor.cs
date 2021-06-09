using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using TMPro.EditorUtilities;

namespace GFrame.Editor
{
    [CustomEditor(typeof(GTextMeshProUGUI))]
    public class GTextMeshProUGUIEditor : TMP_EditorPanelUI
    {

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void DrawExtraSettings()
        {
            base.DrawExtraSettings();
            if (Foldout.extraSettings)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("I18Nkey"));

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
        }
    }
}
