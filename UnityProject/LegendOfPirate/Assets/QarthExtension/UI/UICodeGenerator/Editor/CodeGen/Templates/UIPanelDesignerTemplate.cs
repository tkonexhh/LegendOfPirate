
using System;
using System.IO;

namespace Qarth.Extension
{
    public class UIPanelDesignerTemplate
    {
        public static void Write(string name, string scriptsFolder, string scriptNamespace, PanelCodeInfo panelCodeInfo)
        {
            var scriptFile = scriptsFolder + "/{0}.Designer.cs".FillFormat(name);

            var writer = File.CreateText(scriptFile);

            var root = new RootCode()
                .Using("System")
                .Using("UnityEngine")
                .Using("UnityEngine.UI")
                .Using("Qarth.Extension")
                .Using("Qarth")
                .Using("GFrame.Editor")
                .EmptyLine()
                .Namespace(scriptNamespace.IsTrimNullOrEmpty()
                    ? UIKitSettingData.Namespace
                    : scriptNamespace, ns =>
                {
                    ns.Custom("// Auto Generated Code. Don't Modify This Class ");
                    ns.Class(name, null, true, false, (classScope) =>
                    {
                        classScope.Custom("public const string Name = \"" + name + "\";");
                        classScope.EmptyLine();

                        foreach (var bindInfo in panelCodeInfo.BindInfos)
                        {
                            if (bindInfo.BindScript.Comment.IsNotNullAndEmpty())
                            {
                                classScope.Custom("/// <summary>");
                                classScope.Custom("/// " + bindInfo.BindScript.Comment);
                                classScope.Custom("/// </summary>");
                            }

                            classScope.Custom("[SerializeField]");
                            
                            classScope.Custom("private " + bindInfo.BindScript.ComponentName + " " + string.Format("m_{0}", bindInfo.Name.Replace(" ", "_")) +
                                              ";");
                        }

                        classScope.EmptyLine();

                        //classScope.CustomScope("protected override void ClearUIComponents()", false, (function) =>
                        //{
                        //    foreach (var bindInfo in panelCodeInfo.BindInfos)
                        //    {
                        //        function.Custom(bindInfo.Name + " = null;");
                        //    }

                        //    function.EmptyLine();
                        //    function.Custom("mData = null;");
                        //});

                        classScope.EmptyLine();

                        //classScope.CustomScope("public " + name + "Data Data", false,
                        //    (property) =>
                        //    {
                        //        property.CustomScope("get", false, (getter) => { getter.Custom("return m_Data;"); });
                        //    });

                        //classScope.EmptyLine();


                        //classScope.CustomScope(name + "Data Data", false, (property) =>
                        //{
                        //    property.CustomScope("get", false,
                        //        (getter) =>
                        //        {
                        //            getter.Custom("return m_PrivateData ?? (m_PrivateData = new " + name + "Data());");
                        //        });

                        //    property.CustomScope("set", false, (setter) =>
                        //    {
                        //        //setter.Custom("mUIData = value;");
                        //        setter.Custom("m_PrivateData = value;");
                        //    });
                        //});
                    });
                });

            var codeWriter = new FileCodeWriter(writer);
            root.Gen(codeWriter);
            codeWriter.Dispose();
        }
    }
}