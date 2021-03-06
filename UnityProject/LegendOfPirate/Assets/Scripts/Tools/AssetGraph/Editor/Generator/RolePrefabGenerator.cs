using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AssetGraph;

namespace GameWish.Game
{
    [CustomAssetGenerator("Create Role Prefab", "v0.1", 1)]
    public class RolePrefabGenerator : IAssetGenerator
    {

        public bool CanGenerateAsset(AssetReference asset)
        {
            return true;
        }

        public bool GenerateAsset(AssetReference asset, string generateAssetPath)
        {
            bool generated = false;
            if (!string.IsNullOrEmpty(generateAssetPath))
            {
                var tempGo = AssetDatabase.LoadAssetAtPath<GameObject>(generateAssetPath);//已经生成过了 不在生成了
                // Debug.LogError(tempGo + ":" + generateAssetPath);
                if (tempGo != null)
                {
                    return true;
                }
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(asset.importFrom);
                GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(go);


                //判断是否是主人物模型
                string folderPath = asset.importFrom.Substring(0, asset.importFrom.LastIndexOf("/"));
                var tempP = folderPath.Substring(folderPath.LastIndexOf("/") + 1);
                // Debug.LogError(tempP + "---" + asset.fileName);
                if (string.Equals(tempP, asset.fileName))
                {
                    objSource.AddComponent<RoleModelMonoReference>();
                    var playables = objSource.AddComponent<PlayablesAnimation>();
                    // Debug.LogError("Main Model");

                    var skinRenderers = objSource.GetComponentsInChildren<SkinnedMeshRenderer>();
                    for (int i = 0; i < skinRenderers.Length; i++)
                    {
                        var material = AssetDatabase.LoadAssetAtPath<Material>(FileNameWithoutSuffix(asset.importFrom) + "_Mat.mat");//替换新材质
                        if (material != null)
                        {
                            skinRenderers[i].material = material;
                        }
                    }

                    //找到所有的动画文件
                    string[] allPath = AssetDatabase.FindAssets("t:model", new string[] { folderPath });
                    // Debug.LogError(folderPath + "-----" + allPath.Length);
                    playables.clipsList = new List<AnimationClip>();
                    for (int i = 0; i < allPath.Length; i++)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(allPath[i]);
                        if (path.Contains("@"))
                        {
                            var model = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationClip)) as AnimationClip;
                            if (model != null)
                            {
                                if (model.name.Contains("idle") || model.name.Contains("run") || model.name.Contains("walk") || model.name.Contains("vectory"))
                                {
                                    var setting = AnimationUtility.GetAnimationClipSettings(model);
                                    setting.loopTime = true;
                                    AnimationUtility.SetAnimationClipSettings(model, setting);
                                }

                                // if (model.name.Contains("attack"))//事件添加失败 -- 手动添加事件
                                // {
                                //     Debug.LogError(model.name);
                                //     AnimationEvent animEvent = new AnimationEvent();
                                //     animEvent.functionName = "Attack";
                                //     animEvent.objectReferenceParameter = objSource;
                                //     animEvent.time = 0.35f;
                                //     // model.AddEvent(animEvent);
                                //     AnimationUtility.SetAnimationEvents(model, new AnimationEvent[] { animEvent });
                                //     Debug.LogError(model.events.Length);
                                // }

                                playables.clipsList.Add(model);
                            }

                        }

                    }

                }
                else
                {
                    objSource.AddComponent<CustomShaderFinder>();


                    //处理变体
                    var meshRenderer = objSource.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {

                        var material = AssetDatabase.LoadAssetAtPath<Material>(FileNameWithoutSuffix(asset.importFrom) + "_Mat.mat");//替换新材质
                        if (material != null)
                            meshRenderer.material = material;
                    }
                }

                GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, generateAssetPath);

                UnityEngine.MonoBehaviour.DestroyImmediate(objSource);

                generated = true;
            }

            //移动材质

            return generated;
        }

        public string GetAssetExtension(AssetReference asset)
        {
            return "_Variant.prefab";
        }

        public Type GetAssetType(AssetReference asset)
        {
            return typeof(Material);
        }
        public void OnInspectorGUI(Action onValueChanged)
        {

        }
        public void OnValidate()
        {

        }




        public static string FileNameWithoutSuffix(string name)
        {
            if (name == null)
            {
                return null;
            }

            int endIndex = name.LastIndexOf('.');
            if (endIndex > 0)
            {
                return name.Substring(0, endIndex);
            }
            return name;
        }

    }

}