
using System;
using System.IO;

using UnityEngine;

namespace Qarth.Extension
{
	using UnityEditor;

	//[Serializable]
	public class UIKitSettingData
	{
		//static string mConfigSavedDir
		//{
		//	get { return (Application.dataPath + "/QarthExtensionData/").CreateDirIfNotExists() + "ProjectConfig/"; }
		//}

		//private const string mConfigSavedFileName = "ProjectConfig.json";

		public static string Namespace = "GameWish.Game";

		//public string UIScriptDir = "/Scripts";

		//public string UIPrefabDir = "/Art/UIPrefab";

		//public string DefaultViewControllerScriptDir = "/Scripts/Game";
		
		//public string DefaultViewControllerPrefabDir = "/Art/Prefab";
		
		//public bool IsDefaultNamespace
		//{
		//	get { return Namespace == "Qarth.Extension"; }
		//}
		
		//public static string GetScriptsPath()
		//{
		//	return Load().UIScriptDir;
		//}
		
		public static string GetProjectNamespace()
		{
			return Namespace;
		}
		
		//public static UIKitSettingData Load()
		//{
		//	mConfigSavedDir.CreateDirIfNotExists();

		//	if (!File.Exists(mConfigSavedDir + mConfigSavedFileName))
		//	{
		//		using (var fileStream = File.Create(mConfigSavedDir + mConfigSavedFileName))
		//		{
		//			fileStream.Close();
		//		}
		//	}

		//	var frameworkConfigData =
		//		JsonUtility.FromJson<UIKitSettingData>(File.ReadAllText(mConfigSavedDir + mConfigSavedFileName));

		//	if (frameworkConfigData == null || string.IsNullOrEmpty(frameworkConfigData.Namespace))
		//	{
		//		frameworkConfigData = new UIKitSettingData {Namespace = "Qarth.Extension"};
		//	}

		//	return frameworkConfigData;
		//}

		//public void Save()
		//{
		//	File.WriteAllText(mConfigSavedDir + mConfigSavedFileName,JsonUtility.ToJson(this));
		//	AssetDatabase.Refresh();
		//}
	}
}