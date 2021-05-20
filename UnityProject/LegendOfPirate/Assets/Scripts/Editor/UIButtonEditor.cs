

using UnityEditor.UI;
using UnityEditor;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(UIButton), true)]
    [CanEditMultipleObjects]
    public class UIButtonEditor : ButtonEditor
    {
        SerializedProperty m_ClickScaleProperty;
        SerializedProperty m_SoundAnimeTypeProperty;
        SerializedProperty m_ClickSoundProperty;
        SerializedProperty m_UseDefaultSoundProperty;
        SerializedProperty m_IsSoundEnableProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_ClickScaleProperty = serializedObject.FindProperty("m_ClickScale");
            m_SoundAnimeTypeProperty = serializedObject.FindProperty("m_AnimeType");
            m_ClickSoundProperty = serializedObject.FindProperty("m_SoundType");
            m_UseDefaultSoundProperty = serializedObject.FindProperty("IsNeedOutChange");
            m_IsSoundEnableProperty = serializedObject.FindProperty("m_IsSoundEnable");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_ClickScaleProperty);
            EditorGUILayout.PropertyField(m_SoundAnimeTypeProperty);
            EditorGUILayout.PropertyField(m_ClickSoundProperty);
            EditorGUILayout.PropertyField(m_UseDefaultSoundProperty);
            EditorGUILayout.PropertyField(m_IsSoundEnableProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
