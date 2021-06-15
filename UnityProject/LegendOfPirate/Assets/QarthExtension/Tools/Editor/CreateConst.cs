using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using Qarth;

public class CreateConst
{
    [MenuItem("Tools/创建Const(Layer&Tag)")]
    public static void CreateConstScript()
    {
        var sb = new StringBuilder();

        HandleLayer(sb);
        sb.AppendLine();
        HandleTag(sb);
        //sb.AppendLine();
        //HandleSortingLayer(sb);
        // Debug.LogError(sb.ToString());
        string path = Application.dataPath + "/Scripts/Define/ConstDefine.cs";
        FileHelper.WriteText(path, sb.ToString());
        AssetDatabase.Refresh();
    }

    private static void HandleLayer(StringBuilder sb)
    {
        sb.AppendLine("public class LayerDefine");
        sb.AppendLine("{");


        for (int i = 0; i < 32; i++)
        {
            var name = LayerMask.LayerToName(i);
            name = name
            .Replace(" ", "_")
            .Replace("&", "_")
            .Replace("/", "_")
            .Replace(".", "_")
            .Replace(",", "_")
            .Replace(";", "_")
            .Replace("-", "_");

            if (!string.IsNullOrEmpty(name))
            {
                sb.AppendLine("\tpublic const int LAYER_" + name.ToUpper() + " = " + i + ";");
            }
        }

        sb.AppendLine("}");
    }


    private static void HandleTag(StringBuilder sb)
    {
        sb.AppendLine("public class TagDefine");
        sb.AppendLine("{");

        //处理预定义部分
        sb.AppendLine("\tpublic const string " + "TAG_Untagged".ToUpper() + " = " + "\"Untagged\";");
        sb.AppendLine("\tpublic const string " + "TAG_Respawn".ToUpper() + " = " + "\"Respawn\";");
        sb.AppendLine("\tpublic const string " + "TAG_Finish".ToUpper() + " = " + "\"Finish\";");
        sb.AppendLine("\tpublic const string " + "TAG_EditorOnly".ToUpper() + " = " + "\"EditorOnly\";");
        sb.AppendLine("\tpublic const string " + "TAG_MainCamera".ToUpper() + "= " + "\"MainCamera\";");
        sb.AppendLine("\tpublic const string " + "TAG_Player".ToUpper() + " = " + "\"Player\";");
        sb.AppendLine("\tpublic const string " + "TAG_GameController".ToUpper() + " = " + "\"GameController\";");                    //把一部分内置Tag先写死

        var asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            for (int i = 0; i < asset.Length; i++)
            {
                //创建序列化对象
                var so = new UnityEditor.SerializedObject(asset[i]);
                var tags = so.FindProperty("tags");          //读取具体字段
                for (int j = 0; j < tags.arraySize; ++j)
                {
                    var item = tags.GetArrayElementAtIndex(j).stringValue;
                    sb.AppendLine("\tpublic const string TAG_" + item.ToUpper() + " = \"" + item + "\";"); //添加到模板
                }
            }
        }

        sb.AppendLine("}");
    }

    private static void HandleSortingLayer(StringBuilder sb)
    {
        sb.AppendLine("public class SortingDefine");
        sb.AppendLine("{");
        //处理预定义部分
        sb.AppendLine("\tpublic const string " + "SORTING_Default".ToUpper() + " = " + "\"Default\";");
        var asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            for (int i = 0; i < asset.Length; i++)
            {
                var so = new UnityEditor.SerializedObject(asset[i]);

                var sortings = so.FindProperty("m_SortingLayers");
                for (int j = 0; j < sortings.arraySize; ++j)
                {
                    var item = sortings.GetArrayElementAtIndex(j).stringValue;
                    sb.AppendLine("\tpublic const string SORTING_" + item.ToUpper() + " = " + item + ";");
                }
            }



        }

        sb.AppendLine("}");
    }
}
