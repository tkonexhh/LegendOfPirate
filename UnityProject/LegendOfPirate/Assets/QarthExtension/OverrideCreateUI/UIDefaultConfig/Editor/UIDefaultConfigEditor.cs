using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Qarth.Editor
{
    public class ProjectDefaultConfigEditor : UnityEditor.Editor
    {
        [MenuItem("Assets/GFrame/Config/Build ProjectDefaultConfig")]
        public static void BuildConfig()
        {
            UIDefaultConfig data = null;
            string folderPath = "Assets/Resources/Config/";
            FileHelper.CreateDirctory(EditorUtils.AssetsPath2ABSPath(folderPath));
            string spriteDataPath = folderPath + "ProjectDefaultConfig.asset";
            data = AssetDatabase.LoadAssetAtPath<UIDefaultConfig>(spriteDataPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<UIDefaultConfig>();
                AssetDatabase.CreateAsset(data, spriteDataPath);
                UIDefaultConfig.textConfig.defaultTextFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }
            Log.i("#Create ProjectDefaultConfig In Folder:" + spriteDataPath);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}
