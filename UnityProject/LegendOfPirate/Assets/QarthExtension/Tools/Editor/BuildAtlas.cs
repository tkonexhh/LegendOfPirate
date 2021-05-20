using System.IO;
using Qarth;
using Qarth.Editor;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;


namespace GameWish.Game
{
    public class BulidAtlas
    {
        [MenuItem("Assets/Qarth Builder/Build SpritesAtlasFolders")]
        static void BuidSpritesAtlas()
        {
            string folderPath = EditorUtils.GetSelectedDirAssetsPath();
            DirectoryInfo dInfo = new DirectoryInfo(EditorUtils.AssetsPath2ABSPath(folderPath));
            DirectoryInfo[] subFolders = dInfo.GetDirectories();
            if (subFolders == null || subFolders.Length == 0)
            {
                BuildSpritesAtlas(folderPath);
            }
            else
            {
                for (int i = 0; i < subFolders.Length; ++i)
                {
                    BuildSpritesAtlas(EditorUtils.ABSPath2AssetsPath(subFolders[i].FullName));
                }
            }
        }

        [MenuItem("Assets/Qarth Builder/Build SpritesAtlasItems")]
        static void BuidSpritesAtlasItems()
        {
            string folderPath = EditorUtils.GetSelectedDirAssetsPath();
            DirectoryInfo dInfo = new DirectoryInfo(EditorUtils.AssetsPath2ABSPath(folderPath));
            DirectoryInfo[] subFolders = dInfo.GetDirectories();
            if (subFolders == null || subFolders.Length == 0)
            {
                BuildSpritesAtlas(folderPath, true);
            }
            else
            {
                for (int i = 0; i < subFolders.Length; ++i)
                {
                    BuildSpritesAtlas(EditorUtils.ABSPath2AssetsPath(subFolders[i].FullName), true);
                }
            }
        }

        public static void BuildSpritesAtlas(string folderPath, bool isItefalse = false)
        {
            SpritesData data = null;

            string folderName = PathHelper.FullAssetPath2Name(folderPath);
            string spriteDataPath = folderPath + "/" + folderName + "Atlas" + ".spriteatlas";

            if (!File.Exists(spriteDataPath))
            {
                SpriteAtlas atlas = new SpriteAtlas();
                SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
                {
                    enableRotation = false,
                    enableTightPacking = false,
                    padding = 8,
                };
                atlas.SetPackingSettings(packSet);
                TextureImporterPlatformSettings platform = new TextureImporterPlatformSettings()
                {
                    maxTextureSize = 2048,
                    format = TextureImporterFormat.Automatic,
                    compressionQuality = 50,
                    crunchedCompression = true,
                    textureCompression = TextureImporterCompression.Compressed,
                };
                atlas.SetPlatformSettings(platform);
                AssetDatabase.CreateAsset(atlas, spriteDataPath);
                if (isItefalse)
                {
                    string workPath = EditorUtils.AssetsPath2ABSPath(folderPath);
                    var filePaths = Directory.GetFiles(workPath);
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        string relPath = EditorUtils.ABSPath2AssetsPath(filePaths[i]);
                        UnityEngine.Object objs = AssetDatabase.LoadMainAssetAtPath(relPath);
                        if (objs != null)
                        {
                            if (objs is Sprite || objs is Texture2D)
                            {
                                SpriteAtlasExtensions.Add(atlas, new UnityEngine.Object[] { objs });

                            }
                        }
                    }
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    UnityEngine.Object texture = AssetDatabase.LoadMainAssetAtPath(folderPath);
                    SpriteAtlasExtensions.Add(atlas, new UnityEngine.Object[] { texture });
                    AssetDatabase.SaveAssets();
                }

            }

        }
    }

}