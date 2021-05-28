using GameWish.Game;
using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TestConfig", menuName = "CreateAsset")]//添加这个特性就能在资源窗口右键创建资源
public class TestPanel : ScriptableObject
{
    public bool PlatformHelper;
}
