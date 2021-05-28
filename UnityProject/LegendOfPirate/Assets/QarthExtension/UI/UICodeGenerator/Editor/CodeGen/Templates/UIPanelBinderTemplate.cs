
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
                .Using("Qarth")
                .Using("UniRx")
                .EmptyLine()
                .Namespace(scriptNamespace, nsScope =>
                {
                    nsScope.Class(name + "Data", "UIPanelData", false, false, classScope => { });

                    nsScope.EmptyLine();

                    nsScope.Class(name, null, true, false, classScope =>
                    {
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