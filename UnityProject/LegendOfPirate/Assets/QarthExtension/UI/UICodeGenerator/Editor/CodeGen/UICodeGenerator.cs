
namespace Qarth.Extension
{
    using UnityEngine;
    using UnityEditor;
    using System.IO;

    public class UICodeGenerator
    {
        [MenuItem("Assets/QarthExtension/Create UICode")]
        public static void CreateUICode()
        {
            var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);

            DoCreateCode(objs);
        }

        public static void DoCreateCode(Object[] objs)
        {
            mScriptKitInfo = null;

            var displayProgress = objs.Length > 1;
            if (displayProgress) EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", 0);
            for (var i = 0; i < objs.Length; i++)
            {
                mInstance.CreateCode(objs[i] as GameObject, AssetDatabase.GetAssetPath(objs[i]));
                if (displayProgress)
                    EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", (float)(i + 1) / objs.Length);
            }

            AssetDatabase.Refresh();
            if (displayProgress) EditorUtility.ClearProgressBar();
        }


        private void CreateCode(GameObject obj, string uiPrefabPath)
        {
            if (obj.IsNotNull())
            {
                var prefabType = PrefabUtility.GetPrefabType(obj);
                if (PrefabType.Prefab != prefabType)
                {
                    return;
                }

                var clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                if (null == clone)
                {
                    return;
                }


                var panelCodeInfo = new PanelCodeInfo();

                Debug.Log(clone.name);
                panelCodeInfo.GameObjectName = clone.name.Replace("(clone)", string.Empty);
                BindCollector.SearchBinds(clone.transform, "", panelCodeInfo);
                CreateUIPanelCode(obj, uiPrefabPath, panelCodeInfo);

                UISerializer.StartAddComponent2PrefabAfterCompile(obj);

                HotScriptBind(obj);

                Object.DestroyImmediate(clone);
            }
        }

        private void CreateUIPanelCode(GameObject uiPrefab, string uiPrefabPath, PanelCodeInfo panelCodeInfo)
        {
            if (null == uiPrefab)
                return;

            var behaviourName = uiPrefab.name;

            //var strFilePath = CodeGenUtil.GenSourceFilePathFromPrefabPath(uiPrefabPath, behaviourName);
            string folderDir = Application.dataPath + @"\Scripts\Game\UIScripts\GamePanels\" + behaviourName;
            if (!Directory.Exists(folderDir))
            {
                Directory.CreateDirectory(folderDir);
            }

            var strFilePath = folderDir + @"\" + behaviourName + ".cs";
            //if (mScriptKitInfo.IsNotNull())
            //{
            //    if (File.Exists(strFilePath) == false)
            //    {
            //        if (mScriptKitInfo.Templates.IsNotNull() && mScriptKitInfo.Templates[0].IsNotNull())
            //            mScriptKitInfo.Templates[0].Generate(strFilePath, behaviourName, UIKitSettingData.GetProjectNamespace(), null);
            //    }
            //}
            //else
            {
                if (File.Exists(strFilePath) == false)
                {
                    UIPanelTemplate.Write(behaviourName, strFilePath, UIKitSettingData.Namespace);
                }
            }

            CreateUIPanelDesignerCode(behaviourName, strFilePath, panelCodeInfo);

            CreateUIPanelBinderCode(behaviourName, strFilePath, panelCodeInfo);
            Debug.Log(">>>>>>>Success Create UIPrefab Code: " + behaviourName);
        }

        private void CreateUIPanelDesignerCode(string behaviourName, string uiUIPanelfilePath, PanelCodeInfo panelCodeInfo)
        {
            var dir = uiUIPanelfilePath.Replace(behaviourName + ".cs", "");
            var generateFilePath = dir + behaviourName + ".Designer.cs";
            //if (mScriptKitInfo.IsNotNull())
            //{
            //    if (mScriptKitInfo.Templates.IsNotNull() && mScriptKitInfo.Templates[1].IsNotNull())
            //    {
            //        mScriptKitInfo.Templates[1].Generate(generateFilePath, behaviourName, UIKitSettingData.GetProjectNamespace(), panelCodeInfo);
            //    }
            //    mScriptKitInfo.HotScriptFilePath.CreateDirIfNotExists();
            //    mScriptKitInfo.HotScriptFilePath = mScriptKitInfo.HotScriptFilePath + "/" + behaviourName + mScriptKitInfo.HotScriptSuffix;
            //    if (File.Exists(mScriptKitInfo.HotScriptFilePath) == false && mScriptKitInfo.Templates.IsNotNull() && mScriptKitInfo.Templates[2].IsNotNull())
            //    {
            //        mScriptKitInfo.Templates[2].Generate(mScriptKitInfo.HotScriptFilePath, behaviourName, UIKitSettingData.GetProjectNamespace(), panelCodeInfo);
            //    }
            //}
            //else
            {
                UIPanelDesignerTemplate.Write(behaviourName, dir, UIKitSettingData.GetProjectNamespace(), panelCodeInfo);
            }

            foreach (var elementCodeData in panelCodeInfo.ElementCodeDatas)
            {
                var elementDir = string.Empty;
                elementDir = elementCodeData.BindInfo.BindScript.GetBindType() == BindType.Element
                    ? (dir + behaviourName + "/").CreateDirIfNotExists()
                    : (dir + behaviourName + "/" + "/Components/").CreateDirIfNotExists();
                CreateUIElementCode(elementDir, elementCodeData);
            }
        }

        private void CreateUIPanelBinderCode(string behaviourName, string uiUIPanelfilePath, PanelCodeInfo panelCodeInfo)
        {
            var dir = uiUIPanelfilePath.Replace(behaviourName + ".cs", "");
            var generateFilePath = dir + behaviourName + ".Binder.cs";

            UIPanelBinderTemplate.Write(behaviourName, dir, UIKitSettingData.GetProjectNamespace(), panelCodeInfo);

            //foreach (var elementCodeData in panelCodeInfo.ElementCodeDatas)
            //{
            //    var elementDir = string.Empty;
            //    elementDir = elementCodeData.BindInfo.BindScript.GetBindType() == BindType.Element
            //        ? (dir + behaviourName + "/").CreateDirIfNotExists()
            //        : (Application.dataPath + "/" + UIKitSettingData.GetScriptsPath() + "/Components/").CreateDirIfNotExists();
            //    CreateUIElementCode(elementDir, elementCodeData);
            //}
        }

        private static void CreateUIElementCode(string generateDirPath, ElementCodeInfo elementCodeInfo)
        {
            var panelFilePathWhithoutExt = generateDirPath + elementCodeInfo.BehaviourName;

            if (File.Exists(panelFilePathWhithoutExt + ".cs") == false)
            {
                UIElementCodeTemplate.Generate(panelFilePathWhithoutExt + ".cs",
                    elementCodeInfo.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeInfo);
            }

            UIElementCodeComponentTemplate.Generate(panelFilePathWhithoutExt + ".Designer.cs",
                elementCodeInfo.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeInfo);

            foreach (var childElementCodeData in elementCodeInfo.ElementCodeDatas)
            {
                var elementDir = (panelFilePathWhithoutExt + "/").CreateDirIfNotExists();
                CreateUIElementCode(elementDir, childElementCodeData);
            }
        }

        private static readonly UICodeGenerator mInstance = new UICodeGenerator();



        private static void HotScriptBind(GameObject uiPrefab)
        {
            if (mScriptKitInfo.IsNotNull() && mScriptKitInfo.CodeBind.IsNotNull())
            {
                mScriptKitInfo.CodeBind.Invoke(uiPrefab, mScriptKitInfo.HotScriptFilePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private static ScriptKitInfo mScriptKitInfo;
    }
}