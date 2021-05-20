
namespace Qarth.Extension
{
    using UnityEngine;

    public interface IBaseTemplate
    {
        void Generate(string generateFilePath, string behaviourName, string nameSpace, PanelCodeInfo panelCodeInfo);
    }

    public delegate void ScriptKitCodeBind(GameObject uiPrefab, string filePath);

    /// <summary>
    /// 存储一些ScriptKit相关的信息
    /// </summary>
    public class ScriptKitInfo
    {
        public int HotScriptType;
        public string HotScriptFilePath;
        public string HotScriptSuffix;
        public IBaseTemplate[] Templates;
        public ScriptKitCodeBind CodeBind;
    }
}