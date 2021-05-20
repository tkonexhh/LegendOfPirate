
using UnityEngine;

namespace Qarth.Extension
{
    public class ViewController : MonoBehaviour
    {
        [HideInInspector] public string Namespace = string.Empty;

        [HideInInspector] public string ScriptName;

        [HideInInspector] public string ScriptsFolder = string.Empty;

        [HideInInspector] public bool GeneratePrefab = false;


        [HideInInspector] public string PrefabFolder = string.Empty;
    }
}