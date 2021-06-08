
using System;
using System.IO;

namespace Qarth.Extension
{
    public class UIPanelBinderTemplate
    {
        public static void Write(string name, string scriptsFolder, string scriptNamespace, PanelCodeInfo panelCodeInfo)
        {
            var scriptFile = scriptsFolder + "/{0}.Binder.cs".FillFormat(name);

            if (File.Exists(scriptFile))
            {
                return;
            }

            var writer = File.CreateText(scriptFile);

            var root = new RootCode()
                .Using("UnityEngine")
                .Using("UnityEngine.UI")
                .Using("Qarth.Extension")
                .Using("Qarth")
                .Using("UniRx")
                .EmptyLine()
                .Namespace(scriptNamespace, nsScope =>
                {
                    nsScope.Class(name + "Data", "UIPanelData", false, false, classScope => 
                    {
                        classScope.CustomScope("public " + name + "Data()", false,
                            function =>
                            {
                            });
                    });

                    nsScope.EmptyLine();

                    nsScope.Class(name, null, true, false, classScope =>
                    {
                        classScope.Custom("private " + name + "Data m_PanelData = null;");

                        classScope.EmptyLine();

                        classScope.CustomScope("private void AllocatePanelData(params object[] args)", false,
                            function =>
                            {
                                function.Custom(" m_PanelData = UIPanelData.Allocate<" + name + "Data>();");
                            });

                        classScope.EmptyLine();

                        classScope.CustomScope("private void ReleasePanelData()", false,
                            function =>
                            {
                                function.Custom("ObjectPool<" + name + "Data>.S.Recycle(m_PanelData);");
                            });

                        classScope.EmptyLine();

                        classScope.CustomScope("private void BindModelToUI()", false,
                            function =>
                            {
                            });

                        classScope.EmptyLine();

                        classScope.CustomScope("private void BindUIToModel()", false,
                            function =>
                            {
                            });

                        classScope.EmptyLine();
                    });
                });

            var codeWriter = new FileCodeWriter(writer);
            root.Gen(codeWriter);
            codeWriter.Dispose();
        }
    }
}