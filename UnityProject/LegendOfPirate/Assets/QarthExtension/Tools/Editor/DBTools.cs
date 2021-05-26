using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameWish.Game
{
    public class PlayerPrefTools
    {
        [MenuItem("Tools/Clear Saved Data")]
        static public void ClearSavedData()
        {
            PlayerPrefs.DeleteAll();

            List<string> pathList = GameDataMgr.GetAllDataPaths();

            foreach (string path in pathList)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
