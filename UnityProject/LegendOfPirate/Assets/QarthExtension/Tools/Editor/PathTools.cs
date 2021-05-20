using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GFrame.Editor
{

    public class PathTools
    {
        [MenuItem("Tools/Path/Open Asset Path")]
        private static void OpenAssetPath()
        {
            Execute(Application.dataPath);
        }

        [MenuItem("Tools/Path/Open Peristent Path")]
        private static void OpenPeristentPath()
        {
            Execute(Application.persistentDataPath);
        }

        [MenuItem("Tools/Path/Open StreamingAssets Path")]
        private static void OpenStreamingAssetsPath()
        {
            Execute(Application.streamingAssetsPath);
        }

        [MenuItem("Tools/Path/Open TemporaryCachePath Path")]
        private static void OpenTemporaryCachePathPath()
        {
            Execute(Application.temporaryCachePath);
        }


        /// <summary>
        /// 打开指定路径的文件夹。
        /// </summary>
        /// <param name="folder">要打开的文件夹的路径。</param>
        public static void Execute(string folder)
        {
            folder = string.Format("\"{0}\"", folder);
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    Process.Start("Explorer.exe", folder.Replace('/', '\\'));
                    break;

                case RuntimePlatform.OSXEditor:
                    Process.Start("open", folder);
                    break;

                default:
                    throw new Exception(string.Format("Not support open folder on '{0}' platform.",
                        Application.platform.ToString()));
            }
        }
    }
}
