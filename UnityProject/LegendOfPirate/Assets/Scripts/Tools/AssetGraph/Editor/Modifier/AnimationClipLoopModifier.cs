using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AssetGraph;
using System;
using System.Linq;
using Object = UnityEngine.Object;
using UnityEditor;

[CustomModifier("Set Loop to Animation", typeof(AnimationClip))]
public class AnimationClipLoopModifier : IModifier
{
    [SerializeField] bool m_loopTime;
    public bool IsModified(Object[] assets, List<AssetReference> group)
    {
        Debug.LogError(assets.Length);
        for (int i = 0; i < group.Count; i++)
        {
            Debug.LogError(group[i].path);
        }
        var anim = assets.Where(a => a is AnimationClip).FirstOrDefault() as AnimationClip;
        return true;
    }

    public void Modify(Object[] assets, List<AssetReference> group)
    {
        Debug.LogError(assets.Length);
        for (int i = 0; i < group.Count; i++)
        {
            Debug.LogError(group[i].path);
        }
    }


    public void OnInspectorGUI(Action onValueChanged)
    {
        bool valueChanged = false;

        var newLoopTime = EditorGUILayout.ToggleLeft("Loop Time", m_loopTime);

        if (newLoopTime != m_loopTime)
        {
            m_loopTime = newLoopTime;
            valueChanged = true;
        }

        if (valueChanged)
        {
            onValueChanged();
        }
    }

    public void OnValidate() { }
}
