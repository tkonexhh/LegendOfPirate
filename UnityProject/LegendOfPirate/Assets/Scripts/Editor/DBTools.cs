using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameWish.Game
{
    public class PlayerPrefTools
    {
        [MenuItem("DB Tools/Clear Saved Data")]
        static public void ClearSavedData()
        {
            PlayerPrefs.DeleteAll();

            string path = GameDataHandler.s_path;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
