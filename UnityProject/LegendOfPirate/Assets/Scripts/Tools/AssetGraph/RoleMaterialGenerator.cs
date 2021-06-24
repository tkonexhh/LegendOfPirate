using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AssetGraph;

namespace GameWish.Game
{
    [CustomAssetGenerator("Create Role Material", "v0.1", 1)]
    public class RoleMaterialGenerator : IAssetGenerator
    {
        [SerializeField] private string shaderName = "URP/Character";
        public bool CanGenerateAsset(AssetReference asset)
        {
            return true;
        }

        public bool GenerateAsset(AssetReference asset, string generateAssetPath)
        {
            bool generated = false;

            if (!string.IsNullOrEmpty(generateAssetPath))
            {
                var tempMat = AssetDatabase.LoadAssetAtPath<Material>(generateAssetPath);//已经生成过了 不在生成了
                if (tempMat != null)
                {
                    return false;
                }
                Material material = new Material(Shader.Find(shaderName));
                HandleMaterial(material, asset);
                // Debug.LogError(generateAssetPath);
                AssetDatabase.CreateAsset(material, generateAssetPath);
                generated = true;
            }

            //移动材质

            return generated;
        }

        public string GetAssetExtension(AssetReference asset)
        {
            return "_Mat.mat";
        }

        public Type GetAssetType(AssetReference asset)
        {
            return typeof(Material);
        }
        public void OnInspectorGUI(Action onValueChanged)
        {
            EditorGUILayout.TextField("Material Shader：", shaderName);
        }
        public void OnValidate()
        {

        }


        private void HandleMaterial(Material material, AssetReference asset)
        {
            string filePath = FileNameWithoutSuffix(asset.importFrom);
            string texPath = filePath + "_T.png";
            var mainTexture = AssetDatabase.LoadAssetAtPath<Texture>(texPath);
            if (mainTexture != null)
            {
                material.SetTexture("_MainTex", mainTexture);
            }
            material.SetFloat("_ShadowMid", 0.02f);
            material.SetFloat("_ShadowSmooth", 0.01f);
            material.SetColor("_ShadowColor", new Color(0.08235294f, 0, 0.8196078f, 0));
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