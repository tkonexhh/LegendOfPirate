using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GFrame.Editor
{
    [CustomEditor(typeof(GButton))]
    public class GButtonEditor : UnityEditor.UI.ButtonEditor
    {

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            serializedObject.Update();

            var prop_ScaleAnim = this.serializedObject.FindProperty("scaleAnim");
            EditorGUILayout.PropertyField(prop_ScaleAnim);
            if (prop_ScaleAnim.boolValue)
            {
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("clickDownScale"));
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("normalScale"));
            }

            var prop_Sound = this.serializedObject.FindProperty("sound");
            EditorGUILayout.PropertyField(prop_Sound);
            if (prop_Sound.boolValue)
            {
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("clickEffect"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
